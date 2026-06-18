namespace Senit.Platform.API.GuestStay.Domain.Model.Queries;

/// <summary>
///     Query used to get guest stays, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only guest stays owned by the active hotel.</param>
public record GetAllGuestStaysQuery(string? HotelId = null);
