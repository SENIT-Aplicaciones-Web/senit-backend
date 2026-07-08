using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts Stripe Checkout resources into commands.
/// </summary>
public static class CreateStripeCheckoutSessionCommandFromResourceAssembler
{
    public static CreateStripeSubscriptionCheckoutSessionCommand ToCommandFromResource(
        CreateStripeCheckoutSessionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource), "CreateStripeCheckoutSessionResource cannot be null.");

        return new CreateStripeSubscriptionCheckoutSessionCommand(
            resource.Username,
            resource.Email,
            resource.Password,
            resource.Plan);
    }
}
