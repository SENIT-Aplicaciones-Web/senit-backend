namespace Senit.Platform.API.FrontDesk.Domain.Model.Queries;

/// <summary>
///     Query used to get notifications, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only notifications owned by the active hotel.</param>
public record GetAllNotificationsQuery(string? HotelId = null);
