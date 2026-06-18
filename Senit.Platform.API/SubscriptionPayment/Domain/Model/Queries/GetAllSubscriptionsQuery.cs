namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;

/// <summary>
///     Query used to get subscriptions, optionally filtered by hotel.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only subscriptions owned by the active hotel.</param>
public record GetAllSubscriptionsQuery(string? HotelId = null);
