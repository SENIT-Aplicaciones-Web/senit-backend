using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

namespace Senit.Platform.API.SubscriptionPayment.Application.CommandServices;

/// <summary>
///     Application service contract for simulated subscription checkout use cases.
/// </summary>
public interface ISimulatedSubscriptionCheckoutCommandService
{
    Task<ApplicationResult<SimulatedCheckoutSessionResult>> Handle(
        CreateSimulatedSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default);

    Task<ApplicationResult<SimulatedCheckoutSessionResult>> Handle(
        CompleteSimulatedSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default);

    Task<ApplicationResult<SimulatedCheckoutSessionResult>> GetSession(
        string sessionId,
        CancellationToken cancellationToken = default);
}
