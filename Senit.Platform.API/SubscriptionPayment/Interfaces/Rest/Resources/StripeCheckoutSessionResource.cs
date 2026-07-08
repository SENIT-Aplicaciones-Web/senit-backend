namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned by the Stripe hosted Checkout flow.
/// </summary>
public record StripeCheckoutSessionResource(
    string Id,
    string CheckoutUrl,
    string SuccessUrl,
    string CancelUrl,
    string Plan,
    decimal Amount,
    string Currency,
    string Status,
    string PaymentStatus,
    string CustomerEmail);
