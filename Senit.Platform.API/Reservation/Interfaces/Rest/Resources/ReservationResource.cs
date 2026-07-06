using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a reservation.
/// </summary>
public record ReservationResource(
    [property: OpenApiExample("reservation_roberto_diaz")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("room_standard_101")] string RoomId,
    [property: OpenApiExample("Juan Perez")] string GuestName,
    [property: OpenApiExample("12345678")] string Dni,
    [property: OpenApiExample("987654321")] string Phone,
    [property: OpenApiExample("juan.perez@mail.com")] string? Email,
    [property: OpenApiExample(2)] int GuestsQuantity,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime StartAt,
    [property: OpenApiExample("2026-07-06T18:00:00Z")] DateTime EndAt,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample(4.00)] decimal Hours,
    [property: OpenApiExample(80.00)] decimal ReservationAmount,
    [property: OpenApiExample(20.00)] decimal PrepaidAmount,
    [property: OpenApiExample("cash")] string PaymentMethod,
    [property: OpenApiExample("pending")] string PaymentStatus,
    [property: OpenApiExample(null)] DateTime? PaidAt,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
