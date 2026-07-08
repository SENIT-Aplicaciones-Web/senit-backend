using System.Collections.Concurrent;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Stripe;

/// <summary>
///     In-memory store for Stripe Checkout registration sessions. It avoids creating database records before payment confirmation.
/// </summary>
public class InMemoryStripeCheckoutSessionStore : IStripeCheckoutSessionStore
{
    private readonly ConcurrentDictionary<string, StripeCheckoutRegistrationSession> _sessions = new();

    public void Save(StripeCheckoutRegistrationSession session)
    {
        _sessions[session.Id] = session;
    }

    public bool TryGet(string sessionId, out StripeCheckoutRegistrationSession session)
    {
        return _sessions.TryGetValue(sessionId, out session!);
    }
}
