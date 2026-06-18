namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;

/// <summary>
///     Query used to get subscription payments, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only subscription payments owned by the active hotel.</param>
public record GetAllSubscriptionPaymentsQuery(string? HotelId = null);
