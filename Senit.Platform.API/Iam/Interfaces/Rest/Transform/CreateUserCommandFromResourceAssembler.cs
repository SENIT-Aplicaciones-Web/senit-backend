using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create user resource to a command.
/// </summary>
public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateUserResource cannot be null when converting to command.");

        return new CreateUserCommand(
            resource.HotelId,
            resource.FullName,
            resource.Username,
            resource.Email,
            resource.Password,
            resource.Role,
            resource.Status);
    }
}
