using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for an operational notification.
/// </summary>
public record NotificationResource(
    [property: OpenApiExample("notification_checkout_pending")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("Checkout pendiente")] string Title,
    [property: OpenApiExample("La habitación 204 tiene checkout pendiente")] string Message,
    [property: OpenApiExample("warning")] string Type,
    [property: OpenApiExample("user_maria_rodriguez")] string? CreatedBy,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
