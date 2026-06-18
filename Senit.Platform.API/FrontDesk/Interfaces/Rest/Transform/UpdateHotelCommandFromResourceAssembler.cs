using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update hotel resource to a command.
/// </summary>
public static class UpdateHotelCommandFromResourceAssembler
{
    public static UpdateHotelCommand ToCommandFromResource(string id, UpdateHotelResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateHotelResource cannot be null when converting to command.");

        return new UpdateHotelCommand(
            id,
            resource.Name,
            resource.Ruc,
            resource.Address,
            resource.Phone,
            resource.Email,
            resource.Plan,
            resource.Status);
    }
}
