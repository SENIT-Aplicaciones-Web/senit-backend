using Senit.Platform.API.FrontDesk.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.FrontDesk.Application.Internal.EventHandlers;

/// <summary>
///     Handles the hotel created event.
/// </summary>
public class HotelCreatedEventHandler : IEventHandler<HotelCreatedEvent>
{
    public Task Handle(HotelCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(HotelCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Hotel: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
