namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update an operational notification.
/// </summary>
public class UpdateNotificationResource
{
    public string HotelId { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string? CreatedBy { get; init; }
}
