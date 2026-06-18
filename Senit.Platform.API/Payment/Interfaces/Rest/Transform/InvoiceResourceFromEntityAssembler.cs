using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a invoice entity to a resource.
/// </summary>
public static class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Invoice cannot be null when converting to resource.");

        return new InvoiceResource(
            entity.Id,
            entity.PaymentId,
            entity.Number,
            entity.CustomerName,
            entity.Amount,
            entity.IssuedAt,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
