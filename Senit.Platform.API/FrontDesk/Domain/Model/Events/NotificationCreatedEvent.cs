using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.FrontDesk.Domain.Model.Events;

/// <summary>
///     Event raised when a notification is created.
/// </summary>
public class NotificationCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
