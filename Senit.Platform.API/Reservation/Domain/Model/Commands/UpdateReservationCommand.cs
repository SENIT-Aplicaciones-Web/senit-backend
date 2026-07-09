namespace Senit.Platform.API.Reservation.Domain.Model.Commands;

/// <summary>
///     Command used to update a reservation.
/// </summary>
public record UpdateReservationCommand(
    string Id,
    string HotelId,
    string RoomId,
    string GuestName,
    string Dni,
    string Phone,
    string? Email,
    int GuestsQuantity,
    DateTime StartAt,
    DateTime EndAt,
    string Status,
    decimal Hours,
    decimal ReservationAmount,
    decimal PrepaidAmount,
    string PaymentMethod,
    string PaymentStatus,
    DateTime? PaidAt);
