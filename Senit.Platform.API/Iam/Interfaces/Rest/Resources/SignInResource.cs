namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to authenticate a user.
/// </summary>
public class SignInResource
{
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
