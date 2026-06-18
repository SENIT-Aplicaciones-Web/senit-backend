namespace Senit.Platform.API.FrontDesk.Domain.Model.Commands;

/// <summary>
///     Command used to update an operational notification.
/// </summary>
public record UpdateNotificationCommand(
    string Id,
    string HotelId,
    string Title,
    string Message,
    string Type,
    string? CreatedBy);
