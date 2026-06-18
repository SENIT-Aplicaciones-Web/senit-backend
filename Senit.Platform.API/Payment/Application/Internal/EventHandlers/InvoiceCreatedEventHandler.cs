using Senit.Platform.API.Payment.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Payment.Application.Internal.EventHandlers;

/// <summary>
///     Handles the invoice created event.
/// </summary>
public class InvoiceCreatedEventHandler : IEventHandler<InvoiceCreatedEvent>
{
    public Task Handle(InvoiceCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(InvoiceCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Invoice: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
