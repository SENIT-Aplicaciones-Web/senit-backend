namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Stripe;

/// <summary>
///     Configuration for Stripe hosted Checkout in test or live mode.
/// </summary>
public class StripeCheckoutOptions
{
    public string SecretKey { get; set; } = string.Empty;

    public string FrontendBaseUrl { get; set; } = "http://localhost:5173";

    public string Currency { get; set; } = "pen";
}
