using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create guest resource to a command.
/// </summary>
public static class CreateGuestCommandFromResourceAssembler
{
    public static CreateGuestCommand ToCommandFromResource(CreateGuestResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateGuestResource cannot be null when converting to command.");

        return new CreateGuestCommand(
            resource.FullName,
            resource.Dni,
            resource.Phone,
            resource.Email);
    }
}
