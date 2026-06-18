namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a guest.
/// </summary>
public class CreateGuestResource
{
    public string FullName { get; init; } = string.Empty;

    public string Dni { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string? Email { get; init; }

}
