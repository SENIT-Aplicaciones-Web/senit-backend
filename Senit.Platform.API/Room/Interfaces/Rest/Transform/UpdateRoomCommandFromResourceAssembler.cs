using Senit.Platform.API.Room.Domain.Model.Commands;
using Senit.Platform.API.Room.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Room.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update room resource to a command.
/// </summary>
public static class UpdateRoomCommandFromResourceAssembler
{
    public static UpdateRoomCommand ToCommandFromResource(string id, UpdateRoomResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateRoomResource cannot be null when converting to command.");

        return new UpdateRoomCommand(
            id,
            resource.HotelId,
            resource.Number,
            resource.Floor,
            resource.Type,
            resource.Capacity,
            resource.PricePerHour,
            resource.Status);
    }
}
