namespace Senit.Platform.API.Room.Domain.Model.Queries;

/// <summary>
///     Query used to get rooms, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only rooms owned by the active hotel.</param>
public record GetAllRoomsQuery(string? HotelId = null);
