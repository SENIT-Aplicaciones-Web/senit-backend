using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create payment resource to a command.
/// </summary>
public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreatePaymentResource cannot be null when converting to command.");

        return new CreatePaymentCommand(
            resource.HotelId,
            resource.GuestStayId,
            resource.ReservationId,
            resource.Amount,
            resource.Method,
            resource.Status,
            resource.PaidAt);
    }
}
