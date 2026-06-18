using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;

/// <summary>
///     Represents an operational notification aggregate.
/// </summary>
public class Notification : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string? CreatedBy { get; private set; }

    public Notification()
    {
    }

    public Notification(
        string id,
        string hotelId,
        string title,
        string message,
        string type,
        string? createdBy)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        Title = title;
        Message = message;
        Type = type;
        CreatedBy = createdBy;
    }

    public void Update(
        string hotelId,
        string title,
        string message,
        string type,
        string? createdBy)
    {
        HotelId = hotelId;
        Title = title;
        Message = message;
        Type = type;
        CreatedBy = createdBy;
    }
}
