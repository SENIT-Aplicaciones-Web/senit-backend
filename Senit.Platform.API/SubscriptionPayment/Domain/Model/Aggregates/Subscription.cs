using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;

/// <summary>
///     Represents a subscription aggregate.
/// </summary>
public class Subscription : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string Plan { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public decimal MonthlyAmount { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? EndsAt { get; private set; }

    public Subscription()
    {
    }

    public Subscription(
        string id,
        string hotelId,
        string plan,
        string status,
        decimal monthlyAmount,
        DateTime startedAt,
        DateTime? endsAt)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        Plan = plan;
        Status = status;
        MonthlyAmount = monthlyAmount;
        StartedAt = startedAt;
        EndsAt = endsAt;
    }

    public void Update(
        string hotelId,
        string plan,
        string status,
        decimal monthlyAmount,
        DateTime startedAt,
        DateTime? endsAt)
    {
        HotelId = hotelId;
        Plan = plan;
        Status = status;
        MonthlyAmount = monthlyAmount;
        StartedAt = startedAt;
        EndsAt = endsAt;
    }
}
