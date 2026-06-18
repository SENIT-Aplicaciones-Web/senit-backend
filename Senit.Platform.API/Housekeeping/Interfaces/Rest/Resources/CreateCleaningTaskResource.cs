namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a cleaning task.
/// </summary>
public class CreateCleaningTaskResource
{
    public string HotelId { get; init; } = string.Empty;

    public string RoomId { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;
}
