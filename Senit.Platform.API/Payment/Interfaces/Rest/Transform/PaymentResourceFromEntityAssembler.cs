using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a payment entity to a resource.
/// </summary>
public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(PaymentRecord entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Payment cannot be null when converting to resource.");

        return new PaymentResource(
            entity.Id,
            entity.HotelId,
            entity.GuestStayId,
            entity.ReservationId,
            entity.Amount,
            entity.Method,
            entity.Status,
            entity.PaidAt,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
