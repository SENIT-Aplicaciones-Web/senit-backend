namespace Senit.Platform.API.Payment.Domain.Model.Queries;

/// <summary>
///     Query used to get payments, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only payments owned by the active hotel.</param>
public record GetAllPaymentsQuery(string? HotelId = null);
