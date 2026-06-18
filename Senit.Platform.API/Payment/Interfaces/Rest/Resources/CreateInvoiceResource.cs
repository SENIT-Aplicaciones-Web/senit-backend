namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a invoice.
/// </summary>
public class CreateInvoiceResource
{
    public string PaymentId { get; init; } = string.Empty;

    public string Number { get; init; } = string.Empty;

    public string CustomerName { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public DateTime IssuedAt { get; init; }

}
