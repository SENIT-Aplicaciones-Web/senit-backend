using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts simulated checkout session results into REST resources.
/// </summary>
public static class SimulatedCheckoutSessionResourceFromResultAssembler
{
    public static SimulatedCheckoutSessionResource ToResourceFromResult(SimulatedCheckoutSessionResult result)
    {
        return new SimulatedCheckoutSessionResource(
            result.Id,
            result.CheckoutUrl,
            result.SuccessUrl,
            result.CancelUrl,
            result.Plan,
            result.Amount,
            result.Currency,
            result.Status,
            result.CustomerEmail);
    }
}
