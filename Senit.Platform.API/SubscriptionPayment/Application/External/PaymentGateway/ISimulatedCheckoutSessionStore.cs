namespace Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

/// <summary>
///     Stores simulated checkout sessions outside the database until the payment is confirmed.
/// </summary>
public interface ISimulatedCheckoutSessionStore
{
    void Save(SimulatedCheckoutRegistrationSession session);

    bool TryGet(string sessionId, out SimulatedCheckoutRegistrationSession session);
}
