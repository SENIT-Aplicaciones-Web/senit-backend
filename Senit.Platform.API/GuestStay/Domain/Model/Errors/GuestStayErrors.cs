namespace Senit.Platform.API.GuestStay.Domain.Model.Errors;

/// <summary>
///     GuestStay context error codes used by application services.
/// </summary>
public enum GuestStayErrors
{
    ConsumptionNotFound,
    GuestNotFound,
    StayNotFound,
    InvalidAmount,
    InvalidDateRange,
    InvalidDurationHours,
    StartDateInPast,
    RoomNotAvailable,
    RoomHasActiveStay,
    ReservationOverlap
}
