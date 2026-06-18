using Senit.Platform.API.GuestStay.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.GuestStay.Application.Internal.EventHandlers;

/// <summary>
///     Handles the gueststay created event.
/// </summary>
public class GuestStayCreatedEventHandler : IEventHandler<GuestStayCreatedEvent>
{
    public Task Handle(GuestStayCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(GuestStayCreatedEvent domainEvent)
    {
        Console.WriteLine("Created GuestStay: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
