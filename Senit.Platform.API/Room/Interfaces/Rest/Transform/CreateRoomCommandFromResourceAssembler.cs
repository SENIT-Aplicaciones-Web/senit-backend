using Senit.Platform.API.Room.Domain.Model.Commands;
using Senit.Platform.API.Room.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Room.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create room resource to a command.
/// </summary>
public static class CreateRoomCommandFromResourceAssembler
{
    public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateRoomResource cannot be null when converting to command.");

        return new CreateRoomCommand(
            resource.HotelId,
            resource.Number,
            resource.Floor,
            resource.Type,
            resource.Capacity,
            resource.PricePerHour,
            resource.Status);
    }
}
