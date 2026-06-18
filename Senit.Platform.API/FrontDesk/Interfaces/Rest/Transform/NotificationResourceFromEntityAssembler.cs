using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts a notification entity to a resource.
/// </summary>
public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Notification cannot be null when converting to resource.");

        return new NotificationResource(
            entity.Id,
            entity.HotelId,
            entity.Title,
            entity.Message,
            entity.Type,
            entity.CreatedBy,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
