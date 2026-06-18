using Senit.Platform.API.Reservation.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Reservation.Application.Internal.EventHandlers;

/// <summary>
///     Handles the reservation created event.
/// </summary>
public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
{
    public Task Handle(ReservationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(ReservationCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Reservation: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
