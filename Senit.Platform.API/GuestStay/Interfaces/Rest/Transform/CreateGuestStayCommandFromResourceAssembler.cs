using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create gueststay resource to a command.
/// </summary>
public static class CreateGuestStayCommandFromResourceAssembler
{
    public static CreateGuestStayCommand ToCommandFromResource(CreateGuestStayResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateGuestStayResource cannot be null when converting to command.");

        return new CreateGuestStayCommand(
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
