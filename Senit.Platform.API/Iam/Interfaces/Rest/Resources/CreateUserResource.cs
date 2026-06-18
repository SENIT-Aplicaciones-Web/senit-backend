namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a user.
/// </summary>
public class CreateUserResource
{
    public string HotelId { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public string Username { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

}
