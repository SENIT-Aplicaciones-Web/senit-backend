namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a guest.
/// </summary>
public record GuestResource(
    string Id,
    string FullName,
    string Dni,
    string Phone,
    string? Email,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
