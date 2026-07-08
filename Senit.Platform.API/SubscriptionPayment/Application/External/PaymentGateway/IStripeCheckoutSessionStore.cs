namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Stores checkout registration data outside the database until Stripe confirms the payment.
/// </summary>
public interface IStripeCheckoutSessionStore
{
    void Save(StripeCheckoutRegistrationSession session);

    bool TryGet(string sessionId, out StripeCheckoutRegistrationSession session);
}
