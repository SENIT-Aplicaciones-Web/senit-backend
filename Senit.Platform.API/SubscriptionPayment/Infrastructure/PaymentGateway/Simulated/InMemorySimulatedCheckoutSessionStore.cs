using System.Collections.Concurrent;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Simulated;

/// <summary>
///     In-memory store for simulated checkout sessions. It avoids creating database records before payment confirmation.
/// </summary>
public class InMemorySimulatedCheckoutSessionStore : ISimulatedCheckoutSessionStore
{
    private readonly ConcurrentDictionary<string, SimulatedCheckoutRegistrationSession> _sessions = new();

    public void Save(SimulatedCheckoutRegistrationSession session)
    {
        _sessions[session.Id] = session;
    }

    public bool TryGet(string sessionId, out SimulatedCheckoutRegistrationSession session)
    {
        return _sessions.TryGetValue(sessionId, out session!);
    }
}
