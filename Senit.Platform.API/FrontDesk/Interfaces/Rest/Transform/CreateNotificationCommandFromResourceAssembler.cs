using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create notification resource to a command.
/// </summary>
public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateNotificationResource cannot be null when converting to command.");

        return new CreateNotificationCommand(
            resource.HotelId,
            resource.Title,
            resource.Message,
            resource.Type,
            resource.CreatedBy);
    }
}
