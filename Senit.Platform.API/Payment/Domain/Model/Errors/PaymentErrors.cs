namespace Senit.Platform.API.Payment.Domain.Model.Errors;

/// <summary>
///     Payment context error codes used by application services.
/// </summary>
public enum PaymentErrors
{
    PaymentNotFound,
    InvoiceNotFound,
    InvalidAmount
}
