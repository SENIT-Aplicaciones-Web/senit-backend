namespace Senit.Platform.API.Room.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a room.
/// </summary>
public class UpdateRoomResource
{
    public string HotelId { get; init; } = string.Empty;

    public string Number { get; init; } = string.Empty;

    public int Floor { get; init; }

    public string Type { get; init; } = string.Empty;

    public int Capacity { get; init; }

    public decimal PricePerHour { get; init; }

    public string Status { get; init; } = string.Empty;

}
