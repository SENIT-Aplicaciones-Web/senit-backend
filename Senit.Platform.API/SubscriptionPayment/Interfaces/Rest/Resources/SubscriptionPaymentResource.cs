using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a subscription payment.
/// </summary>
public record SubscriptionPaymentResource(
    [property: OpenApiExample("subscription_payment_yape_reservation")] string Id,
    [property: OpenApiExample("subscription_basic_senit_lima")] string SubscriptionId,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("Basic")] string Plan,
    [property: OpenApiExample(99.00)] decimal Amount,
    [property: OpenApiExample("card")] string Method,
    [property: OpenApiExample("paid")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime? PaidAt,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
