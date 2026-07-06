using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a user.
/// </summary>
public record UserResource(
    [property: OpenApiExample("user_maria_rodriguez")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("Maria Rodriguez")] string FullName,
    [property: OpenApiExample("mrodriguez")] string Username,
    [property: OpenApiExample("maria.rodriguez@senit.pe")] string Email,
    [property: OpenApiExample("Recepcion123")] string Password,
    [property: OpenApiExample("FRONT_DESK")] string Role,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
