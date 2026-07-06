using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a subscription.
/// </summary>
public record SubscriptionResource(
    [property: OpenApiExample("subscription_basic_senit_lima")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("Basic")] string Plan,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample(99.00)] decimal MonthlyAmount,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime StartedAt,
    [property: OpenApiExample(null)] DateTime? EndsAt,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
