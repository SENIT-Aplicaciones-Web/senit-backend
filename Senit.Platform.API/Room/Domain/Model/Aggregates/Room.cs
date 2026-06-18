using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Room.Domain.Model.Aggregates;

/// <summary>
///     Represents a room aggregate.
/// </summary>
public class Room : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public int Floor { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    public decimal PricePerHour { get; private set; }
    public string Status { get; private set; } = string.Empty;

    public Room()
    {
    }

    public Room(
        string id,
        string hotelId,
        string number,
        int floor,
        string type,
        int capacity,
        decimal pricePerHour,
        string status)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        Number = number;
        Floor = floor;
        Type = type;
        Capacity = capacity;
        PricePerHour = pricePerHour;
        Status = status;
    }

    public void Update(
        string hotelId,
        string number,
        int floor,
        string type,
        int capacity,
        decimal pricePerHour,
        string status)
    {
        HotelId = hotelId;
        Number = number;
        Floor = floor;
        Type = type;
        Capacity = capacity;
        PricePerHour = pricePerHour;
        Status = status;
    }
}
