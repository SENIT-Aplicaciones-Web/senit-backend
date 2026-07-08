namespace Senit.Platform.API.Reservation.Domain.Model.Commands;

/// <summary>
///     Command used to create a reservation.
/// </summary>
public record CreateReservationCommand(
    string HotelId,
    string RoomId,
    string GuestName,
    string Dni,
    string Phone,
    string? Email,
    int GuestsQuantity,
    string? AdditionalGuestsJson,
    DateTime StartAt,
    DateTime EndAt,
    string Status,
    decimal PrepaidAmount,
    string PaymentMethod,
    string PaymentStatus,
    DateTime? PaidAt);
