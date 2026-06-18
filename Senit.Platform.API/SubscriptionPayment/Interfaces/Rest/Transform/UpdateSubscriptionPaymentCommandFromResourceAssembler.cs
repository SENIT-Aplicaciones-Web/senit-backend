using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update subscriptionpayment resource to a command.
/// </summary>
public static class UpdateSubscriptionPaymentCommandFromResourceAssembler
{
    public static UpdateSubscriptionPaymentCommand ToCommandFromResource(string id, UpdateSubscriptionPaymentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateSubscriptionPaymentResource cannot be null when converting to command.");

        return new UpdateSubscriptionPaymentCommand(
            id,
            resource.SubscriptionId,
            resource.HotelId,
            resource.Plan,
            resource.Amount,
            resource.Method,
            resource.Status,
            resource.PaidAt);
    }
}
