namespace Senit.Platform.API.FrontDesk.Domain.Model.Commands;

/// <summary>
///     Command used to create a hotel.
/// </summary>
public record CreateHotelCommand(
    string Name,
    string Ruc,
    string Address,
    string Phone,
    string Email,
    string Plan,
    string Status);
