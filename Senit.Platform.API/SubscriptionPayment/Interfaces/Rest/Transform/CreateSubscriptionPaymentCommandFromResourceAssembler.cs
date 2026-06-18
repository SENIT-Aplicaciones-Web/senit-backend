using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create subscriptionpayment resource to a command.
/// </summary>
public static class CreateSubscriptionPaymentCommandFromResourceAssembler
{
    public static CreateSubscriptionPaymentCommand ToCommandFromResource(CreateSubscriptionPaymentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateSubscriptionPaymentResource cannot be null when converting to command.");

        return new CreateSubscriptionPaymentCommand(
            resource.SubscriptionId,
            resource.HotelId,
            resource.Plan,
            resource.Amount,
            resource.Method,
            resource.Status,
            resource.PaidAt);
    }
}
