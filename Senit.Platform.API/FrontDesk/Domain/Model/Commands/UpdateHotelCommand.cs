namespace Senit.Platform.API.FrontDesk.Domain.Model.Commands;

/// <summary>
///     Command used to update a hotel.
/// </summary>
public record UpdateHotelCommand(
    string Id,
    string Name,
    string Ruc,
    string Address,
    string Phone,
    string Email,
    string Plan,
    string Status);
