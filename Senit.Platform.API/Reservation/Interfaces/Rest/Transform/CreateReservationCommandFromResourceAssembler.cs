using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Reservation.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create reservation resource to a command.
/// </summary>
public static class CreateReservationCommandFromResourceAssembler
{
    public static CreateReservationCommand ToCommandFromResource(CreateReservationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateReservationResource cannot be null when converting to command.");

        return new CreateReservationCommand(
            resource.HotelId,
            resource.RoomId,
            resource.GuestName,
            resource.Dni,
            resource.Phone,
            resource.Email,
            resource.GuestsQuantity,
            resource.StartAt,
            resource.EndAt,
            resource.Status,
            resource.PrepaidAmount,
            resource.PaymentMethod,
            resource.PaymentStatus,
            resource.PaidAt);
    }
}
