using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update payment resource to a command.
/// </summary>
public static class UpdatePaymentCommandFromResourceAssembler
{
    public static UpdatePaymentCommand ToCommandFromResource(string id, UpdatePaymentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdatePaymentResource cannot be null when converting to command.");

        return new UpdatePaymentCommand(
            id,
            resource.HotelId,
            resource.GuestStayId,
            resource.ReservationId,
            resource.Amount,
            resource.Method,
            resource.Status,
            resource.PaidAt);
    }
}
