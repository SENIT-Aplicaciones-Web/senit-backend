using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Domain.Model.Queries;
using Senit.Platform.API.Iam.Interfaces.Acl;

namespace Senit.Platform.API.Iam.Application.Acl;

/// <summary>
///     Anti corruption facade for the IAM bounded context.
/// </summary>
public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : IIamContextFacade
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
}
