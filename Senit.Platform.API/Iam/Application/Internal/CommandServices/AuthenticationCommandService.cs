using FrontDeskHotel = Senit.Platform.API.FrontDesk.Domain.Model.Aggregates.Hotel;
using Senit.Platform.API.FrontDesk.Domain.Repositories;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Domain.Model.Errors;
using Senit.Platform.API.Iam.Domain.Model.ValueObjects;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;

namespace Senit.Platform.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Authentication command service without JWT for the current project advance.
/// </summary>
public class AuthenticationCommandService(
    IUserRepository userRepository,
    IHotelRepository hotelRepository,
    IHotelStaffMemberRepository hotelStaffMemberRepository,
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork) : IAuthenticationCommandService
{
    /// <inheritdoc />
    public async Task<ApplicationResult<User>> Handle(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);

        if (user == null || user.Password != command.Password || user.Status == "inactive")
            return ApplicationResult<User>.Failure(nameof(IamErrors.InvalidCredentials), StatusCodes.Status401Unauthorized);

        var activeAssignment = await hotelStaffMemberRepository.FindFirstActiveAssignmentByUserIdAsync(
            user.Id,
            cancellationToken);

        if (activeAssignment == null)
            return ApplicationResult<User>.Failure(nameof(IamErrors.UserNotAssignedToHotel), StatusCodes.Status403Forbidden);

        user.ChangeDefaultHotel(activeAssignment.HotelId);
        user.ChangeRole(activeAssignment.Role);

        return ApplicationResult<User>.Success(user);
    }

    /// <inheritdoc />
    public async Task<ApplicationResult<User>> Handle(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailAddress.Normalize(command.Email);
        var username = command.Username.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(normalizedEmail) ||
            string.IsNullOrWhiteSpace(command.Password))
            return ApplicationResult<User>.Failure(nameof(IamErrors.InvalidRequest), StatusCodes.Status400BadRequest);

        if (await userRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken: cancellationToken))
            return ApplicationResult<User>.Failure(nameof(IamErrors.DuplicateEmail), StatusCodes.Status409Conflict);

        var hotel = new FrontDeskHotel(
            Guid.NewGuid().ToString(),
            $"Hotel de {username}",
            string.Empty,
            string.Empty,
            string.Empty,
            normalizedEmail,
            "Basic",
            "active");

        await hotelRepository.AddAsync(hotel, cancellationToken);

        var user = new User(
            Guid.NewGuid().ToString(),
            hotel.Id,
            username,
            username,
            normalizedEmail,
            command.Password,
            "ADMIN",
            "active");

        await userRepository.AddAsync(user, cancellationToken);

        var staffAssignment = new HotelStaffMember(
            Guid.NewGuid().ToString(),
            hotel.Id,
            user.Id,
            "ADMIN",
            "active");

        await hotelStaffMemberRepository.AddAsync(staffAssignment, cancellationToken);

        var subscription = new Subscription(
            Guid.NewGuid().ToString(),
            hotel.Id,
            "Basic",
            "active",
            49.00m,
            DateTime.UtcNow,
            null);

        await subscriptionRepository.AddAsync(subscription, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<User>.Created(user);
    }
}
