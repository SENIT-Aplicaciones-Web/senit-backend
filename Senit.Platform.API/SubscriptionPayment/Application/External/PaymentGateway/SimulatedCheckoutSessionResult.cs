namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Result returned by the simulated checkout gateway.
/// </summary>
public record SimulatedCheckoutSessionResult(
    string Id,
    string CheckoutUrl,
    string SuccessUrl,
    string CancelUrl,
    string Plan,
    decimal Amount,
    string Currency,
    string Status,
    string CustomerEmail);
