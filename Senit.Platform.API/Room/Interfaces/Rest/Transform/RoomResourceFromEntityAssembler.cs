using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Room.Interfaces.Rest.Transform;

/// <summary>
///     Converts a room entity to a resource.
/// </summary>
public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(RoomEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Room cannot be null when converting to resource.");

        return new RoomResource(
            entity.Id,
            entity.HotelId,
            entity.Number,
            entity.Floor,
            entity.Type,
            entity.Capacity,
            entity.PricePerHour,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
