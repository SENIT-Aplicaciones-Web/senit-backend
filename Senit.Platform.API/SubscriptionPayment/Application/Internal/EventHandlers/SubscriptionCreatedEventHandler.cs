using Senit.Platform.API.SubscriptionPayment.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.SubscriptionPayment.Application.Internal.EventHandlers;

/// <summary>
///     Handles the subscription created event.
/// </summary>
public class SubscriptionCreatedEventHandler : IEventHandler<SubscriptionCreatedEvent>
{
    public Task Handle(SubscriptionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(SubscriptionCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Subscription: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
