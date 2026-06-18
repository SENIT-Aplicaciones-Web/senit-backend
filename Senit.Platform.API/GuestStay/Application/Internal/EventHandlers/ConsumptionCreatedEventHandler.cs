using Senit.Platform.API.GuestStay.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.GuestStay.Application.Internal.EventHandlers;

/// <summary>
///     Handles the consumption created event.
/// </summary>
public class ConsumptionCreatedEventHandler : IEventHandler<ConsumptionCreatedEvent>
{
    public Task Handle(ConsumptionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(ConsumptionCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Consumption: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
