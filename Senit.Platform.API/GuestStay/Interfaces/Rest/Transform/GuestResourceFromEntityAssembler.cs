using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a guest entity to a resource.
/// </summary>
public static class GuestResourceFromEntityAssembler
{
    public static GuestResource ToResourceFromEntity(Guest entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Guest cannot be null when converting to resource.");

        return new GuestResource(
            entity.Id,
            entity.FullName,
            entity.Dni,
            entity.Phone,
            entity.Email,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
