using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Events;

/// <summary>
///     Event raised when a subscriptionpayment is created.
/// </summary>
public class SubscriptionPaymentCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
