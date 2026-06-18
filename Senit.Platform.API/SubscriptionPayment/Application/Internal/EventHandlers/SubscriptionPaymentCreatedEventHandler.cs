using Senit.Platform.API.SubscriptionPayment.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.SubscriptionPayment.Application.Internal.EventHandlers;

/// <summary>
///     Handles the subscriptionpayment created event.
/// </summary>
public class SubscriptionPaymentCreatedEventHandler : IEventHandler<SubscriptionPaymentCreatedEvent>
{
    public Task Handle(SubscriptionPaymentCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(SubscriptionPaymentCreatedEvent domainEvent)
    {
        Console.WriteLine("Created SubscriptionPayment: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
