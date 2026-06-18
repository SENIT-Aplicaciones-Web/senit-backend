namespace Senit.Platform.API.GuestStay.Domain.Model.Queries;

/// <summary>
///     Query used to get guests, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only guests owned by the active hotel.</param>
public record GetAllGuestsQuery(string? HotelId = null);
