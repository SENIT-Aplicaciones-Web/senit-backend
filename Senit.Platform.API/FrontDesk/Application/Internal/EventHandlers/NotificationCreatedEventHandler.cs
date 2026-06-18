using Senit.Platform.API.FrontDesk.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.FrontDesk.Application.Internal.EventHandlers;

/// <summary>
///     Handles the notification created event.
/// </summary>
public class NotificationCreatedEventHandler : IEventHandler<NotificationCreatedEvent>
{
    public Task Handle(NotificationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(NotificationCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Notification: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
