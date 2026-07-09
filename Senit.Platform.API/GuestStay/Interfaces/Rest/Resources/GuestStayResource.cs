using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a guest stay.
/// </summary>
public record GuestStayResource(
    [property: OpenApiExample("stay_room_deluxe_204_july")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("room_standard_101")] string RoomId,
    [property: OpenApiExample("guest_juan_perez")] string GuestId,
    [property: OpenApiExample("Juan Perez")] string GuestName,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime StartAt,
    [property: OpenApiExample("2026-07-06T18:00:00Z")] DateTime ExpectedEndAt,
    [property: OpenApiExample(null)] DateTime? ActualEndAt,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample(80.00)] decimal BaseAmount,
    [property: OpenApiExample(0.00)] decimal AdditionalAmount,
    [property: OpenApiExample(0.00)] decimal PrepaidAmount,
    [property: OpenApiExample(80.00)] decimal TotalAmount,
    [property: OpenApiExample("pending")] string PaymentStatus,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
