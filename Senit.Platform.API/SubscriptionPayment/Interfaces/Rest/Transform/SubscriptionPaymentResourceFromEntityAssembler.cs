using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a subscriptionpayment entity to a resource.
/// </summary>
public static class SubscriptionPaymentResourceFromEntityAssembler
{
    public static SubscriptionPaymentResource ToResourceFromEntity(SubscriptionPaymentRecord entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "SubscriptionPayment cannot be null when converting to resource.");

        return new SubscriptionPaymentResource(
            entity.Id,
            entity.SubscriptionId,
            entity.HotelId,
            entity.Plan,
            entity.Amount,
            entity.Method,
            entity.Status,
            entity.PaidAt,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
