namespace Senit.Platform.API.GuestStay.Domain.Model.Queries;

/// <summary>
///     Query used to get consumptions, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only consumptions owned by the active hotel.</param>
public record GetAllConsumptionsQuery(string? HotelId = null);
