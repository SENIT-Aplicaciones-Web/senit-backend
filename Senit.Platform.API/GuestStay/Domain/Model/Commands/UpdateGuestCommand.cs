namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to update a guest.
/// </summary>
public record UpdateGuestCommand(
    string Id,
    string FullName,
    string Dni,
    string Phone,
    string? Email);
