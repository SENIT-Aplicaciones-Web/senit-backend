using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update notification resource to a command.
/// </summary>
public static class UpdateNotificationCommandFromResourceAssembler
{
    public static UpdateNotificationCommand ToCommandFromResource(string id, UpdateNotificationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateNotificationResource cannot be null when converting to command.");

        return new UpdateNotificationCommand(
            id,
            resource.HotelId,
            resource.Title,
            resource.Message,
            resource.Type,
            resource.CreatedBy);
    }
}
