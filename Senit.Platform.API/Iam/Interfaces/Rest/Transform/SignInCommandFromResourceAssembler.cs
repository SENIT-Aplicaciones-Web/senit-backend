using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts a sign in resource into a command.
/// </summary>
public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "SignInResource cannot be null when converting to command.");

        return new SignInCommand(resource.Email, resource.Password);
    }
}
