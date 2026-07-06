using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a cleaning task.
/// </summary>
public record CleaningTaskResource(
    [property: OpenApiExample("cleaning_room_204_checkout")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("room_standard_101")] string RoomId,
    [property: OpenApiExample("Limpieza posterior al checkout")] string Description,
    [property: OpenApiExample("pending")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
