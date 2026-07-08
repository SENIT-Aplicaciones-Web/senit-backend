using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update gueststay resource to a command.
/// </summary>
public static class UpdateGuestStayCommandFromResourceAssembler
{
    public static UpdateGuestStayCommand ToCommandFromResource(string id, UpdateGuestStayResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateGuestStayResource cannot be null when converting to command.");

        return new UpdateGuestStayCommand(
            id,
            resource.HotelId,
            resource.RoomId,
            resource.GuestId,
            resource.GuestName,
            resource.AdditionalGuestsJson,
            resource.StartAt,
            resource.ExpectedEndAt,
            resource.ActualEndAt,
            resource.Status,
            resource.BaseAmount,
            resource.AdditionalAmount,
            resource.PrepaidAmount,
            resource.TotalAmount,
            resource.PaymentStatus);
    }
}
