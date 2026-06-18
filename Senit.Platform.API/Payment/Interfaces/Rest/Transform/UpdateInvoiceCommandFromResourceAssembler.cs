using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update invoice resource to a command.
/// </summary>
public static class UpdateInvoiceCommandFromResourceAssembler
{
    public static UpdateInvoiceCommand ToCommandFromResource(string id, UpdateInvoiceResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateInvoiceResource cannot be null when converting to command.");

        return new UpdateInvoiceCommand(
            id,
            resource.PaymentId,
            resource.Number,
            resource.CustomerName,
            resource.Amount,
            resource.IssuedAt);
    }
}
