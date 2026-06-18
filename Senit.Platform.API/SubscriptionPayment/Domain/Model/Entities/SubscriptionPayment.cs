namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Entities;

/// <summary>
///     Payment history for subscription changes.
/// </summary>
public class SubscriptionPayment
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Plan { get; set; } = "Basic";
    public decimal Amount { get; set; }
    public string Status { get; set; } = "completed";
    public DateTime PaidAt { get; set; }
}
