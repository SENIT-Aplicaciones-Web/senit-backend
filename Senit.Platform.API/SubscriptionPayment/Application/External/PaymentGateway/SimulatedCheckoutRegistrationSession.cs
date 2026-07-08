namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Temporary simulated checkout registration data kept outside the database until payment confirmation.
/// </summary>
public class SimulatedCheckoutRegistrationSession
{
    public string Id { get; }
    public string Username { get; }
    public string Email { get; }
    public string Password { get; }
    public string Plan { get; }
    public decimal Amount { get; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }
    public string? HotelId { get; private set; }
    public string? SubscriptionId { get; private set; }

    public SimulatedCheckoutRegistrationSession(
        string id,
        string username,
        string email,
        string password,
        string plan,
        decimal amount)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Plan = plan;
        Amount = amount;
        Status = "open";
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkCompleted(string hotelId, string subscriptionId, DateTime completedAt)
    {
        HotelId = hotelId;
        SubscriptionId = subscriptionId;
        Status = "completed";
        CompletedAt = completedAt;
    }
}
