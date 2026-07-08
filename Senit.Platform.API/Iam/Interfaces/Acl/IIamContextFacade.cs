namespace Senit.Platform.API.Iam.Interfaces.Acl;

/// <summary>
///     Anti corruption facade exposed by the IAM bounded context.
/// </summary>
public interface IIamContextFacade
{
    /// <summary>
    ///     Creates a user in IAM and returns its identifier when successful.
    /// </summary>
    Task<string> CreateUser(
        string hotelId,
        string fullName,
        string username,
        string email,
        string password,
        string role,
        string status,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetches a user's default hotel id without exposing IAM domain objects.
    /// </summary>
    Task<string> FetchDefaultHotelIdByUserId(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetches a user's role without exposing IAM domain objects.
    /// </summary>
    Task<string> FetchRoleByUserId(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes a user assignment from a hotel without exposing IAM command services.
    /// </summary>
    Task<bool> RemoveUserFromHotel(string hotelId, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether an email can start a hotel administrator registration.
    /// </summary>
    Task<bool> CanRegisterHotelAdministrator(
        string email,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Activates the administrator assigned to a hotel without exposing IAM domain objects.
    /// </summary>
    Task<bool> ActivateHotelAdministrator(
        string hotelId,
        CancellationToken cancellationToken = default);

}
