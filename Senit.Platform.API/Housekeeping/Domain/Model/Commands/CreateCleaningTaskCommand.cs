namespace Senit.Platform.API.Housekeeping.Domain.Model.Commands;

/// <summary>
///     Command used to create a cleaning task.
/// </summary>
public record CreateCleaningTaskCommand(
    string HotelId,
    string RoomId,
    string Description,
    string Status);
