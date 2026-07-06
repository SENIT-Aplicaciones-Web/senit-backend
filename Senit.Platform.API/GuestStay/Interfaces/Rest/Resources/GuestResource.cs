using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a guest.
/// </summary>
public record GuestResource(
    [property: OpenApiExample("guest_juan_perez")] string Id,
    [property: OpenApiExample("Juan Perez")] string FullName,
    [property: OpenApiExample("12345678")] string Dni,
    [property: OpenApiExample("987654321")] string Phone,
    [property: OpenApiExample("juan.perez@mail.com")] string? Email,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
