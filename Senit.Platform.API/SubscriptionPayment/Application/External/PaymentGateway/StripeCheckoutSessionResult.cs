namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Result returned by Stripe hosted Checkout operations.
/// </summary>
public record StripeCheckoutSessionResult(
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
