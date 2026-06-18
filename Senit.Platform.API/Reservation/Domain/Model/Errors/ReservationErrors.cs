namespace Senit.Platform.API.Reservation.Domain.Model.Errors;

/// <summary>
///     Reservation context error codes used by application services.
/// </summary>
public enum ReservationErrors
{
    ReservationNotFound,
    RoomNotFound,
    RoomInMaintenance,
    InvalidGuestsQuantity,
    ReservationOverlap,
    InvalidDateRange,
    InvalidDurationHours
}
