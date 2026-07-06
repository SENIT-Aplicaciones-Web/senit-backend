using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a hotel.
/// </summary>
public record HotelResource(
    [property: OpenApiExample("hotel_senit_lima")] string Id,
    [property: OpenApiExample("Hotel Senit Lima")] string Name,
    [property: OpenApiExample("20601234567")] string Ruc,
    [property: OpenApiExample("Av Arequipa 123 Lima")] string Address,
    [property: OpenApiExample("987654321")] string Phone,
    [property: OpenApiExample("contacto@senit.pe")] string Email,
    [property: OpenApiExample("Basic")] string Plan,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
