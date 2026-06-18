namespace Senit.Platform.API.FrontDesk.Domain.Model.Commands;

/// <summary>
///     Command used to create an operational notification.
/// </summary>
public record CreateNotificationCommand(
    string HotelId,
    string Title,
    string Message,
    string Type,
    string? CreatedBy);
