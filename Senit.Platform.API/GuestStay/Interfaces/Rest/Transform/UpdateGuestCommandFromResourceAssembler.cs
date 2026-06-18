using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update guest resource to a command.
/// </summary>
public static class UpdateGuestCommandFromResourceAssembler
{
    public static UpdateGuestCommand ToCommandFromResource(string id, UpdateGuestResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateGuestResource cannot be null when converting to command.");

        return new UpdateGuestCommand(
            id,
            resource.FullName,
            resource.Dni,
            resource.Phone,
            resource.Email);
    }
}
