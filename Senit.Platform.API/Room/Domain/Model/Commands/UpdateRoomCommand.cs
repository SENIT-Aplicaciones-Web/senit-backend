namespace Senit.Platform.API.Room.Domain.Model.Commands;

/// <summary>
///     Command used to update a room.
/// </summary>
public record UpdateRoomCommand(
    string Id,
    string HotelId,
    string Number,
    int Floor,
    string Type,
    int Capacity,
    decimal PricePerHour,
    string Status);
