using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a consumption entity to a resource.
/// </summary>
public static class ConsumptionResourceFromEntityAssembler
{
    public static ConsumptionResource ToResourceFromEntity(Consumption entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Consumption cannot be null when converting to resource.");

        return new ConsumptionResource(
            entity.Id,
            entity.GuestStayId,
            entity.Description,
            entity.Quantity,
            entity.UnitPrice,
            entity.Amount,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
