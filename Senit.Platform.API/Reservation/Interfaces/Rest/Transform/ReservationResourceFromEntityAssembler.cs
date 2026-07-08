using Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Interfaces.Rest.Transform;

/// <summary>
///     Converts a reservation entity to a resource.
/// </summary>
public static class ReservationResourceFromEntityAssembler
{
    public static ReservationResource ToResourceFromEntity(HotelReservation entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "Reservation cannot be null when converting to resource.");

        return new ReservationResource(
            entity.Id,
            entity.HotelId,
            entity.RoomId,
            entity.GuestName,
            entity.Dni,
            entity.Phone,
            entity.Email,
            entity.GuestsQuantity,
            entity.AdditionalGuestsJson,
            entity.StartAt,
            entity.EndAt,
            entity.Status,
            entity.Hours,
            entity.ReservationAmount,
            entity.PrepaidAmount,
            entity.PaymentMethod,
            entity.PaymentStatus,
            entity.PaidAt,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
