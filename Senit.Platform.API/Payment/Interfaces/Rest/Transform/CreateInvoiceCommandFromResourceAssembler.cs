using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create invoice resource to a command.
/// </summary>
public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateInvoiceResource cannot be null when converting to command.");

        return new CreateInvoiceCommand(
            resource.PaymentId,
            resource.Number,
            resource.CustomerName,
            resource.Amount,
            resource.IssuedAt);
    }
}
