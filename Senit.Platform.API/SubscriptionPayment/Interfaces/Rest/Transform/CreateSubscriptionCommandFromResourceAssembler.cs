using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create subscription resource to a command.
/// </summary>
public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateSubscriptionResource cannot be null when converting to command.");

        return new CreateSubscriptionCommand(
            resource.HotelId,
            resource.Plan,
            resource.Status,
            resource.MonthlyAmount,
            resource.StartedAt,
            resource.EndsAt);
    }
}
