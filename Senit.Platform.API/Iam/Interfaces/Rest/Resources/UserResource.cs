namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a user.
/// </summary>
public record UserResource(
    string Id,
    string HotelId,
    string FullName,
    string Username,
    string Email,
    string Password,
    string Role,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
