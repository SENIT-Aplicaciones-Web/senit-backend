namespace Senit.Platform.API.Housekeeping.Domain.Model.Commands;

/// <summary>
///     Command used to update a cleaning task.
/// </summary>
public record UpdateCleaningTaskCommand(
    string Id,
    string HotelId,
    string RoomId,
    string Description,
    string Status);
