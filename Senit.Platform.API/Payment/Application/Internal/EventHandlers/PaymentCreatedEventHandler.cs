using Senit.Platform.API.Payment.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Payment.Application.Internal.EventHandlers;

/// <summary>
///     Handles the payment created event.
/// </summary>
public class PaymentCreatedEventHandler : IEventHandler<PaymentCreatedEvent>
{
    public Task Handle(PaymentCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(PaymentCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Payment: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
