using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a payment.
/// </summary>
public record PaymentResource(
    [property: OpenApiExample("payment_yape_reservation")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("stay_room_deluxe_204_july")] string? GuestStayId,
    [property: OpenApiExample("reservation_roberto_diaz")] string? ReservationId,
    [property: OpenApiExample(80.00)] decimal Amount,
    [property: OpenApiExample("cash")] string Method,
    [property: OpenApiExample("paid")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime? PaidAt,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
