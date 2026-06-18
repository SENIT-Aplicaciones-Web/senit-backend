using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;

/// <summary>
///     Represents a subscriptionpayment aggregate.
/// </summary>
public class SubscriptionPaymentRecord : AuditableEntity
{
    public string SubscriptionId { get; private set; } = string.Empty;
    public string HotelId { get; private set; } = string.Empty;
    public string Plan { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Method { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime? PaidAt { get; private set; }

    public SubscriptionPaymentRecord()
    {
    }

    public SubscriptionPaymentRecord(
        string id,
        string subscriptionId,
        string hotelId,
        string plan,
        decimal amount,
        string method,
        string status,
        DateTime? paidAt)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        SubscriptionId = subscriptionId;
        HotelId = hotelId;
        Plan = plan;
        Amount = amount;
        Method = method;
        Status = status;
        PaidAt = paidAt;
    }

    public void Update(
        string subscriptionId,
        string hotelId,
        string plan,
        decimal amount,
        string method,
        string status,
        DateTime? paidAt)
    {
        SubscriptionId = subscriptionId;
        HotelId = hotelId;
        Plan = plan;
        Amount = amount;
        Method = method;
        Status = status;
        PaidAt = paidAt;
    }
}
