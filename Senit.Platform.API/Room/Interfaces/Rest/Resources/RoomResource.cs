using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Room.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a room.
/// </summary>
public record RoomResource(
    [property: OpenApiExample("room_standard_101")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("101")] string Number,
    [property: OpenApiExample(1)] int Floor,
    [property: OpenApiExample("Standard")] string Type,
    [property: OpenApiExample(2)] int Capacity,
    [property: OpenApiExample(20.00)] decimal PricePerHour,
    [property: OpenApiExample("available")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
