using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a gueststay entity to a resource.
/// </summary>
public static class GuestStayResourceFromEntityAssembler
{
    public static GuestStayResource ToResourceFromEntity(GuestStayRecord entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "GuestStay cannot be null when converting to resource.");

        return new GuestStayResource(
            entity.Id,
            entity.HotelId,
            entity.RoomId,
            entity.GuestId,
            entity.GuestName,
            entity.AdditionalGuestsJson,
            entity.StartAt,
            entity.ExpectedEndAt,
            entity.ActualEndAt,
            entity.Status,
            entity.BaseAmount,
            entity.AdditionalAmount,
            entity.PrepaidAmount,
            entity.TotalAmount,
            entity.PaymentStatus,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
