using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned after a successful authentication.
/// </summary>
public record AuthenticatedUserResource(
    [property: OpenApiExample("user_maria_rodriguez")] string Id,
    [property: OpenApiExample("hotel_senit_lima")] string HotelId,
    [property: OpenApiExample("Maria Rodriguez")] string FullName,
    [property: OpenApiExample("mrodriguez")] string Username,
    [property: OpenApiExample("maria.rodriguez@senit.pe")] string Email,
    [property: OpenApiExample("FRONT_DESK")] string Role,
    [property: OpenApiExample("active")] string Status,
    [property: OpenApiExample("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9")] string Token);
