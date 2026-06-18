namespace Senit.Platform.API.Reservation.Domain.Model.Queries;

/// <summary>
///     Query used to get reservations, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only reservations owned by the active hotel.</param>
public record GetAllReservationsQuery(string? HotelId = null);
