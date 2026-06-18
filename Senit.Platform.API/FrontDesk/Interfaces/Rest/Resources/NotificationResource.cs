namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for an operational notification.
/// </summary>
public record NotificationResource(
    string Id,
    string HotelId,
    string Title,
    string Message,
    string Type,
    string? CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
