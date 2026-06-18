using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Payment.Domain.Model.Aggregates;

/// <summary>
///     Represents a invoice aggregate.
/// </summary>
public class Invoice : AuditableEntity
{
    public string PaymentId { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTime IssuedAt { get; private set; }

    public Invoice()
    {
    }

    public Invoice(
        string id,
        string paymentId,
        string number,
        string customerName,
        decimal amount,
        DateTime issuedAt)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        PaymentId = paymentId;
        Number = number;
        CustomerName = customerName;
        Amount = amount;
        IssuedAt = issuedAt;
    }

    public void Update(
        string paymentId,
        string number,
        string customerName,
        decimal amount,
        DateTime issuedAt)
    {
        PaymentId = paymentId;
        Number = number;
        CustomerName = customerName;
        Amount = amount;
        IssuedAt = issuedAt;
    }
}
