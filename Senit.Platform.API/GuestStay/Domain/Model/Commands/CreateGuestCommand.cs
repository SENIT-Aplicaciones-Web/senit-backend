namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to create a guest.
/// </summary>
public record CreateGuestCommand(
    string FullName,
    string Dni,
    string Phone,
    string? Email);
