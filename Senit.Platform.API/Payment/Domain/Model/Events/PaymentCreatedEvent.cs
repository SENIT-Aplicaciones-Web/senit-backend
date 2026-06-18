using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Payment.Domain.Model.Events;

/// <summary>
///     Event raised when a payment is created.
/// </summary>
public class PaymentCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
