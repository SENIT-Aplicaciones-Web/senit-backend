using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Domain.Model.Queries;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Iam.Interfaces.Acl;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Iam.Application.Acl;

/// <summary>
///     Anti corruption facade for the IAM bounded context.
/// </summary>
public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService,
    IUserRepository userRepository,
    IHotelStaffMemberRepository hotelStaffMemberRepository,
    IUnitOfWork unitOfWork) : IIamContextFacade
{
    /// <summary>
    ///     Creates a user through the IAM bounded context.
    /// </summary>
    public async Task<string> CreateUser(
        string hotelId,
        string fullName,
        string username,
        string email,
        string password,
        string role,
        string status,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(hotelId, fullName, username, email, password, role, status);
        var result = await userCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value?.Id ?? string.Empty : string.Empty;
    }

    /// <summary>
    ///     Gets the default hotel identifier assigned to a user.
    /// </summary>
    public async Task<string> FetchDefaultHotelIdByUserId(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return user?.HotelId ?? string.Empty;
    }

    /// <summary>
    ///     Gets the current role assigned to a user.
    /// </summary>
    public async Task<string> FetchRoleByUserId(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return user?.Role ?? string.Empty;
    }

    /// <summary>
    ///     Removes a user assignment from a hotel through the IAM bounded context.
    /// </summary>
    public async Task<bool> RemoveUserFromHotel(string hotelId, string userId, CancellationToken cancellationToken = default)
    {
        var command = new RemoveUserFromHotelCommand(hotelId, userId);
        var result = await userCommandService.Handle(command, cancellationToken);
        return result.IsSuccess;
    }


    /// <summary>
    ///     Checks whether a hotel registration can be started with the requested email.
    /// </summary>
    public async Task<bool> CanRegisterHotelAdministrator(
        string email,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await userRepository.FindByEmailAsync(email, cancellationToken);
        if (existingUser is null) return true;

        var hasActiveAssignment = await hotelStaffMemberRepository.HasActiveAssignmentAsync(
            existingUser.Id,
            cancellationToken);

        return existingUser.Status != "active" || !hasActiveAssignment;
    }

    /// <summary>
    ///     Activates the administrator assigned to a hotel after checkout confirmation.
    /// </summary>
    public async Task<bool> ActivateHotelAdministrator(
        string hotelId,
        CancellationToken cancellationToken = default)
    {
        var assignment = await hotelStaffMemberRepository.FindFirstAssignmentByHotelIdAndRoleAsync(
            hotelId,
            "ADMIN",
            cancellationToken);

        if (assignment is null) return false;

        var user = await userRepository.FindByIdAsync(assignment.UserId, cancellationToken);
        if (user is null) return false;

        user.Update(
            hotelId,
            user.FullName,
            user.Username,
            user.Email,
            user.Password,
            "ADMIN",
            "active");

        assignment.Update("ADMIN", "active");
        userRepository.Update(user);
        hotelStaffMemberRepository.Update(assignment);
        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}
