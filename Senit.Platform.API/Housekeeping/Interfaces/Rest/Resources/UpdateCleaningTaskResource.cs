namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a cleaning task internally.
/// </summary>
public class UpdateCleaningTaskResource
{
    public string HotelId { get; init; } = string.Empty;

    public string RoomId { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;
}
