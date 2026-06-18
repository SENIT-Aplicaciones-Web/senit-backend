namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a hotel.
/// </summary>
public record HotelResource(
    string Id,
    string Name,
    string Ruc,
    string Address,
    string Phone,
    string Email,
    string Plan,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
