using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Reservation.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update reservation resource to a command.
/// </summary>
public static class UpdateReservationCommandFromResourceAssembler
{
    public static UpdateReservationCommand ToCommandFromResource(string id, UpdateReservationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateReservationResource cannot be null when converting to command.");

        return new UpdateReservationCommand(
            id,
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
            resource.Hours,
            resource.ReservationAmount,
            resource.PrepaidAmount,
            resource.PaymentMethod,
            resource.PaymentStatus,
            resource.PaidAt);
    }
}
