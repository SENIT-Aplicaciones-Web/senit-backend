using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Transform;

/// <summary>
///     Converts cleaning task entities to resources.
/// </summary>
public static class CleaningTaskResourceFromEntityAssembler
{
    public static CleaningTaskResource ToResourceFromEntity(CleaningTask entity)
    {
        return new CleaningTaskResource(
            entity.Id,
            entity.HotelId,
            entity.RoomId,
            entity.Description,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
