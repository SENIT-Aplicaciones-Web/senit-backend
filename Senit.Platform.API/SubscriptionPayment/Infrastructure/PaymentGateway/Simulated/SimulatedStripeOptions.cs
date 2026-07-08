namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Simulated;

/// <summary>
///     Configuration for the simulated Stripe checkout flow.
/// </summary>
public class SimulatedStripeOptions
{
    public string FrontendBaseUrl { get; set; } = "http://localhost:5173";
}
