using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

namespace Senit.Platform.API.SubscriptionPayment.Application.CommandServices;

/// <summary>
///     Application service contract for Stripe subscription checkout use cases.
/// </summary>
public interface IStripeSubscriptionCheckoutCommandService
{
    Task<ApplicationResult<StripeCheckoutSessionResult>> Handle(
        CreateStripeSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default);

    Task<ApplicationResult<StripeCheckoutSessionResult>> GetSession(
        string sessionId,
        CancellationToken cancellationToken = default);
}
