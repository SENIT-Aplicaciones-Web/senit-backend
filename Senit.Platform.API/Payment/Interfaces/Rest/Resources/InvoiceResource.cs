using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for an invoice.
/// </summary>
public record InvoiceResource(
    [property: OpenApiExample("invoice_payment_yape")] string Id,
    [property: OpenApiExample("payment_yape_reservation")] string PaymentId,
    [property: OpenApiExample("B001-000123")] string Number,
    [property: OpenApiExample("Juan Perez")] string CustomerName,
    [property: OpenApiExample(80.00)] decimal Amount,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime IssuedAt,
    [property: OpenApiExample("2026-07-06T14:00:00Z")] DateTime CreatedAt,
    [property: OpenApiExample("2026-07-06T15:00:00Z")] DateTime? UpdatedAt);
