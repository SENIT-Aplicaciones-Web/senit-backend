namespace Senit.Platform.API.Housekeeping.Domain.Model.Queries;

/// <summary>
///     Query used to get cleaning tasks, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only cleaning tasks owned by the active hotel.</param>
public record GetAllCleaningTasksQuery(string? HotelId = null);
