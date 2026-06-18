namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a guest.
/// </summary>
public class UpdateGuestResource
{
    public string FullName { get; init; } = string.Empty;

    public string Dni { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string? Email { get; init; }

}
