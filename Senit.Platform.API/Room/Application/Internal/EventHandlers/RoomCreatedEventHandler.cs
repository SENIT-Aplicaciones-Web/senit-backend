using Senit.Platform.API.Room.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Room.Application.Internal.EventHandlers;

/// <summary>
///     Handles the room created event.
/// </summary>
public class RoomCreatedEventHandler : IEventHandler<RoomCreatedEvent>
{
    public Task Handle(RoomCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(RoomCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Room: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
