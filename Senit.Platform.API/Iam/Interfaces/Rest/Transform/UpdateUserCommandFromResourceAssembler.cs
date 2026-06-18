using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update user resource to a command.
/// </summary>
public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(string id, UpdateUserResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateUserResource cannot be null when converting to command.");

        return new UpdateUserCommand(
            id,
            resource.HotelId,
            resource.FullName,
            resource.Username,
            resource.Email,
            resource.Password,
            resource.Role,
            resource.Status);
    }
}
