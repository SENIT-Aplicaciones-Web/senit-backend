namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a cleaning task.
/// </summary>
public record CleaningTaskResource(
    string Id,
    string HotelId,
    string RoomId,
    string Description,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
