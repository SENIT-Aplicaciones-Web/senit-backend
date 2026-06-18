using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update subscription resource to a command.
/// </summary>
public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(string id, UpdateSubscriptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateSubscriptionResource cannot be null when converting to command.");

        return new UpdateSubscriptionCommand(
            id,
            resource.HotelId,
            resource.Plan,
            resource.Status,
            resource.MonthlyAmount,
            resource.StartedAt,
            resource.EndsAt);
    }
}
