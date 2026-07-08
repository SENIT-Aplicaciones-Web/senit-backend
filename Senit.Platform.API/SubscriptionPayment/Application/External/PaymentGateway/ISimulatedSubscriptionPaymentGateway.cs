namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Outbound gateway contract for the simulated Stripe subscription checkout flow.
/// </summary>
public interface ISimulatedSubscriptionPaymentGateway
{
    SimulatedCheckoutSessionResult CreateSession(
        string sessionId,
        string plan,
        decimal amount,
        string customerEmail);

    string BuildSuccessUrl(string sessionId);
}
