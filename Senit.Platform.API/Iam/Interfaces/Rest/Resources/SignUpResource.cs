namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to register a new administrator and hotel.
/// </summary>
public class SignUpResource
{
    public string Username { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
