namespace Senit.Platform.API.Room.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a room.
/// </summary>
public record RoomResource(
    string Id,
    string HotelId,
    string Number,
    int Floor,
    string Type,
    int Capacity,
    decimal PricePerHour,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
