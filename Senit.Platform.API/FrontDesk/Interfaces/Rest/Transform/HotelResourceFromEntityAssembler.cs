using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts a hotel entity to a resource.
/// </summary>
public static class HotelResourceFromEntityAssembler
{
    public static HotelResource ToResourceFromEntity(Hotel entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Hotel cannot be null when converting to resource.");

        return new HotelResource(
            entity.Id,
            entity.Name,
            entity.Ruc,
            entity.Address,
            entity.Phone,
            entity.Email,
            entity.Plan,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
