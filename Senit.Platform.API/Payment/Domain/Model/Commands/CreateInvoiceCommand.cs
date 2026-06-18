namespace Senit.Platform.API.Payment.Domain.Model.Commands;

/// <summary>
///     Command used to create a invoice.
/// </summary>
public record CreateInvoiceCommand(
    string PaymentId,
    string Number,
    string CustomerName,
    decimal Amount,
    DateTime IssuedAt);
