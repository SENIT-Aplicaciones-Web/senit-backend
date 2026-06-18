using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler used to convert a sign-up resource into a sign-up command.
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignUpResource" /> to a <see cref="SignUpCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The sign-up resource.
    /// </param>
    /// <returns>
    ///     A sign-up command.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when the resource is null.
    /// </exception>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource), "SignUpResource cannot be null when converting to command.");

        return new SignUpCommand(
            resource.Username,
            resource.Email,
            resource.Password);
    }
}
