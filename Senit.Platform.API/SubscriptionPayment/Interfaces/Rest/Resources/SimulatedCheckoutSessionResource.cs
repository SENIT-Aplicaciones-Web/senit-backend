namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned by the simulated Stripe checkout flow.
/// </summary>
public record SimulatedCheckoutSessionResource(
    string Id,
    string CheckoutUrl,
    string SuccessUrl,
    string CancelUrl,
    string Plan,
    decimal Amount,
    string Currency,
    string Status,
    string CustomerEmail);
