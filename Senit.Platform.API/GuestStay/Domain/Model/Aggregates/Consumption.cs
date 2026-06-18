using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.GuestStay.Domain.Model.Aggregates;

/// <summary>
///     Represents a consumption aggregate.
/// </summary>
public class Consumption : AuditableEntity
{
    public string GuestStayId { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Amount { get; private set; }

    public Consumption()
    {
    }

    public Consumption(
        string id,
        string guestStayId,
        string description,
        int quantity,
        decimal unitPrice,
        decimal amount)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        GuestStayId = guestStayId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
    }

    public void Update(
        string guestStayId,
        string description,
        int quantity,
        decimal unitPrice,
        decimal amount)
    {
        GuestStayId = guestStayId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
    }
}
