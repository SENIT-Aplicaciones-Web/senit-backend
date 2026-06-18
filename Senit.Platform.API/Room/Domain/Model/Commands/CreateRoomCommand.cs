namespace Senit.Platform.API.Room.Domain.Model.Commands;

/// <summary>
///     Command used to create a room.
/// </summary>
public record CreateRoomCommand(
    string HotelId,
    string Number,
    int Floor,
    string Type,
    int Capacity,
    decimal PricePerHour,
    string Status);
