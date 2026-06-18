using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;

/// <summary>
///     Represents a cleaning task generated when a room enters cleaning state.
/// </summary>
public class CleaningTask : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string RoomId { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;

    public CleaningTask()
    {
    }

    public CleaningTask(
        string id,
        string hotelId,
        string roomId,
        string description,
        string status)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        RoomId = roomId;
        Description = description;
        Status = status;
    }

    public void Update(
        string hotelId,
        string roomId,
        string description,
        string status)
    {
        HotelId = hotelId;
        RoomId = roomId;
        Description = description;
        Status = status;
    }
}
