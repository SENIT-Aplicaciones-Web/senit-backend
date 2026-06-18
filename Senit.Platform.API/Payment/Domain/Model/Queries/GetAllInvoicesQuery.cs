namespace Senit.Platform.API.Payment.Domain.Model.Queries;

/// <summary>
///     Query used to get invoices, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only invoices owned by the active hotel.</param>
public record GetAllInvoicesQuery(string? HotelId = null);
