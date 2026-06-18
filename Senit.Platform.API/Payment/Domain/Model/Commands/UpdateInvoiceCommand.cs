namespace Senit.Platform.API.Payment.Domain.Model.Commands;

/// <summary>
///     Command used to update a invoice.
/// </summary>
public record UpdateInvoiceCommand(
    string Id,
    string PaymentId,
    string Number,
    string CustomerName,
    decimal Amount,
    DateTime IssuedAt);
