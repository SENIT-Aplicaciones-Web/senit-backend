using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a subscription entity to a resource.
/// </summary>
public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Subscription cannot be null when converting to resource.");

        return new SubscriptionResource(
            entity.Id,
            entity.HotelId,
            entity.Plan,
            entity.Status,
            entity.MonthlyAmount,
            entity.StartedAt,
            entity.EndsAt,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
