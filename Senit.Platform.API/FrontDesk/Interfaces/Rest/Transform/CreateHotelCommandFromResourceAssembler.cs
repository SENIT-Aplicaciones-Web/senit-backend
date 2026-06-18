using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create hotel resource to a command.
/// </summary>
public static class CreateHotelCommandFromResourceAssembler
{
    public static CreateHotelCommand ToCommandFromResource(CreateHotelResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateHotelResource cannot be null when converting to command.");

        return new CreateHotelCommand(
            resource.Name,
            resource.Ruc,
            resource.Address,
            resource.Phone,
            resource.Email,
            resource.Plan,
            resource.Status);
    }
}
