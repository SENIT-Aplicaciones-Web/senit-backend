using Senit.Platform.API.GuestStay.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.GuestStay.Application.Internal.EventHandlers;

/// <summary>
///     Handles the guest created event.
/// </summary>
public class GuestCreatedEventHandler : IEventHandler<GuestCreatedEvent>
{
    public Task Handle(GuestCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(GuestCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Guest: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
