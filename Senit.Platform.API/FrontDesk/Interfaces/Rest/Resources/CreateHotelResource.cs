namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a hotel.
/// </summary>
public class CreateHotelResource
{
    public string Name { get; init; } = string.Empty;

    public string Ruc { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Plan { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

}
