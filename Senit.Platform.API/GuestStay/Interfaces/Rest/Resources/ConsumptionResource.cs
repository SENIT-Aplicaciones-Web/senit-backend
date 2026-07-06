using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a consumption.
/// </summary>
public record ConsumptionResource(
    [property: OpenApiExample("consumption_water_room_204")] string Id,
    [property: OpenApiExample("stay_room_deluxe_204_july")] string GuestStayId,
    [property: OpenApiExample("Agua mineral")] string Description,
    [property: OpenApiExample(2)] int Quantity,
    [property: OpenApiExample(5.50)] decimal UnitPrice,
    [property: OpenApiExample(11.00)] decimal Amount,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
