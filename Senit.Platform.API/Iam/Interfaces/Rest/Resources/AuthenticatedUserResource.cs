namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned after a successful basic sign in.
/// </summary>
public record AuthenticatedUserResource(
    string Id,
    string HotelId,
    string FullName,
    string Username,
    string Email,
    string Role,
    string Status);
