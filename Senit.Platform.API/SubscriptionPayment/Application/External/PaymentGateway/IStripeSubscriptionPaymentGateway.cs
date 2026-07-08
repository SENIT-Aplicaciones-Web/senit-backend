namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Outbound gateway contract for Stripe hosted Checkout subscription sessions.
/// </summary>
public interface IStripeSubscriptionPaymentGateway
{
    Task<StripeCheckoutSessionResult?> CreateSubscriptionCheckoutSessionAsync(
        string plan,
        decimal amount,
        string customerEmail,
        CancellationToken cancellationToken = default);

    Task<StripeCheckoutSessionResult?> RetrieveCheckoutSessionAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
}
