namespace Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a reservation.
/// </summary>
public record ReservationResource(
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
    DateTime? PaidAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
