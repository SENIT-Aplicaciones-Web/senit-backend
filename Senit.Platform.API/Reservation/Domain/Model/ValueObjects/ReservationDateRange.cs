namespace Senit.Platform.API.Reservation.Domain.Model.ValueObjects;

/// <summary>
///     Represents the date range requested for a reservation.
/// </summary>
/// <param name="StartAt">Reservation start date.</param>
/// <param name="EndAt">Reservation end date.</param>
public readonly record struct ReservationDateRange(DateTime StartAt, DateTime EndAt)
{
    public bool IsValid() => StartAt < EndAt;
}
