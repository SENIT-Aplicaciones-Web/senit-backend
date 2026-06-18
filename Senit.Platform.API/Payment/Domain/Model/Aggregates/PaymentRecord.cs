using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Payment.Domain.Model.Aggregates;

/// <summary>
///     Represents a payment aggregate.
/// </summary>
public class PaymentRecord : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string? GuestStayId { get; private set; }
    public string? ReservationId { get; private set; }
    public decimal Amount { get; private set; }
    public string Method { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime? PaidAt { get; private set; }

    public PaymentRecord()
    {
    }

    public PaymentRecord(
        string id,
        string hotelId,
        string? guestStayId,
        string? reservationId,
        decimal amount,
        string method,
        string status,
        DateTime? paidAt)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        GuestStayId = guestStayId;
        ReservationId = reservationId;
        Amount = amount;
        Method = method;
        Status = status;
        PaidAt = paidAt;
    }

    public void Update(
        string hotelId,
        string? guestStayId,
        string? reservationId,
        decimal amount,
        string method,
        string status,
        DateTime? paidAt)
    {
        HotelId = hotelId;
        GuestStayId = guestStayId;
        ReservationId = reservationId;
        Amount = amount;
        Method = method;
        Status = status;
        PaidAt = paidAt;
    }
}
