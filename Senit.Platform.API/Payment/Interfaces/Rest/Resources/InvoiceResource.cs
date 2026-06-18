namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a invoice.
/// </summary>
public record InvoiceResource(
    string Id,
    string PaymentId,
    string Number,
    string CustomerName,
    decimal Amount,
    DateTime IssuedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
