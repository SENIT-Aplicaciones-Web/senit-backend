namespace Senit.Platform.API.Payment.Domain.Model.Entities;

/// <summary>
///     Payment entity for reservations, stays and subscriptions when required.
/// </summary>
public class Payment
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int? ReservationId { get; set; }
    public int? StayId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = "cash";
    public string Status { get; set; } = "completed";
    public DateTime PaidAt { get; set; }
}
