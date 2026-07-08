using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts simulated checkout resources into commands.
/// </summary>
public static class CreateSimulatedCheckoutSessionCommandFromResourceAssembler
{
    public static CreateSimulatedSubscriptionCheckoutSessionCommand ToCommandFromResource(
        CreateSimulatedCheckoutSessionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource), "CreateSimulatedCheckoutSessionResource cannot be null.");

        return new CreateSimulatedSubscriptionCheckoutSessionCommand(
            resource.Username,
            resource.Email,
            resource.Password,
            resource.Plan);
    }
}
