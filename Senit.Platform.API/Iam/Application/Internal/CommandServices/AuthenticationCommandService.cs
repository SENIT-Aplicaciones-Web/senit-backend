using Senit.Platform.API.FrontDesk.Interfaces.Acl;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Application.Internal.OutboundServices;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Domain.Model.Errors;
using Senit.Platform.API.Iam.Domain.Model.ValueObjects;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Acl;

namespace Senit.Platform.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Authentication command service that returns JWT bearer tokens for IAM sessions.
/// </summary>
public class AuthenticationCommandService(
    IUserRepository userRepository,
    IHotelStaffMemberRepository hotelStaffMemberRepository,
    IFrontDeskContextFacade frontDeskContextFacade,
    ISubscriptionPaymentContextFacade subscriptionPaymentContextFacade,
    IUnitOfWork unitOfWork,
    ITokenService tokenService) : IAuthenticationCommandService
{
    /// <summary>
    ///     Authenticates a user and returns the created access token.
    /// </summary>
    public async Task<ApplicationResult<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);

        if (user == null || user.Password != command.Password || user.Status == "inactive")
            return ApplicationResult<(User user, string token)>.Failure(nameof(IamErrors.InvalidCredentials), StatusCodes.Status401Unauthorized);

        var activeAssignment = await hotelStaffMemberRepository.FindFirstActiveAssignmentByUserIdAsync(
            user.Id,
            cancellationToken);

        if (activeAssignment == null)
            return ApplicationResult<(User user, string token)>.Failure(nameof(IamErrors.UserNotAssignedToHotel), StatusCodes.Status403Forbidden);

        user.ChangeDefaultHotel(activeAssignment.HotelId);
        user.ChangeRole(activeAssignment.Role);

        return ApplicationResult<(User user, string token)>.Success((user, tokenService.GenerateToken(user)));
    }

    /// <summary>
    ///     Registers a hotel administrator and returns the created access token.
    /// </summary>
    public async Task<ApplicationResult<(User user, string token)>> Handle(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailAddress.Normalize(command.Email);
        var username = command.Username.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(normalizedEmail) ||
            string.IsNullOrWhiteSpace(command.Password))
            return ApplicationResult<(User user, string token)>.Failure(nameof(IamErrors.InvalidRequest), StatusCodes.Status400BadRequest);

        if (await userRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken: cancellationToken))
            return ApplicationResult<(User user, string token)>.Failure(nameof(IamErrors.DuplicateEmail), StatusCodes.Status409Conflict);

        var hotelId = await frontDeskContextFacade.CreateHotel(
            $"Hotel de {username}",
            string.Empty,
            string.Empty,
            string.Empty,
            normalizedEmail,
            "Basic",
            "active",
            cancellationToken);

        if (string.IsNullOrWhiteSpace(hotelId))
            return ApplicationResult<(User user, string token)>.Failure(nameof(IamErrors.InvalidRequest), StatusCodes.Status400BadRequest);

        var user = new User(
            Guid.NewGuid().ToString(),
            hotelId,
            username,
            username,
            normalizedEmail,
            command.Password,
            "ADMIN",
            "active");

        await userRepository.AddAsync(user, cancellationToken);

        var staffAssignment = new HotelStaffMember(
            Guid.NewGuid().ToString(),
            hotelId,
            user.Id,
            "ADMIN",
            "active");

        await hotelStaffMemberRepository.AddAsync(staffAssignment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        await subscriptionPaymentContextFacade.CreateSubscription(
            hotelId,
            "Basic",
            "active",
            49.00m,
            DateTime.UtcNow,
            null,
            cancellationToken);

        return ApplicationResult<(User user, string token)>.Created((user, tokenService.GenerateToken(user)));
    }

    /// <summary>
    ///     Updates the password for the user identified by email.
    /// </summary>
    public async Task<ApplicationResult<bool>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailAddress.Normalize(command.Email);
        var newPassword = command.NewPassword?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedEmail))
            return ApplicationResult<bool>.Failure(nameof(IamErrors.InvalidRequest), StatusCodes.Status400BadRequest);

        if (newPassword.Length < 6)
            return ApplicationResult<bool>.Failure(nameof(IamErrors.InvalidPassword), StatusCodes.Status400BadRequest);

        var user = await userRepository.FindByEmailAsync(normalizedEmail, cancellationToken);
        if (user == null)
            return ApplicationResult<bool>.Failure(nameof(IamErrors.UserNotFound), StatusCodes.Status404NotFound);

        user.ChangePassword(newPassword);
        userRepository.Update(user);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
