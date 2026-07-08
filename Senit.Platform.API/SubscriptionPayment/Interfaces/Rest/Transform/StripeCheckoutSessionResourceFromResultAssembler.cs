using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts Stripe Checkout session results into REST resources.
/// </summary>
public static class StripeCheckoutSessionResourceFromResultAssembler
{
    public static StripeCheckoutSessionResource ToResourceFromResult(StripeCheckoutSessionResult result)
    {
        return new StripeCheckoutSessionResource(
            result.Id,
            result.CheckoutUrl,
            result.SuccessUrl,
            result.CancelUrl,
            result.Plan,
            result.Amount,
            result.Currency,
            result.Status,
            result.PaymentStatus,
            result.CustomerEmail);
    }
}
