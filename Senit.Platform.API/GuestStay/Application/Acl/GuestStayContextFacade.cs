using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.GuestStay.Interfaces.Acl;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Application.Acl;

/// <summary>
///     Anti corruption facade implementation for the GuestStay bounded context.
/// </summary>
public class GuestStayContextFacade(
    IGuestStayRepository guestStayRepository,
    IUnitOfWork unitOfWork) : IGuestStayContextFacade
{
    /// <summary>
    ///     Checks whether a room has an active guest stay.
    /// </summary>
    public async Task<bool> HasActiveStayByRoomId(
        string roomId,
        string? excludedStayId = null,
        CancellationToken cancellationToken = default)
    {
        return await guestStayRepository.ExistsActiveStayByRoomIdAsync(roomId, excludedStayId, cancellationToken);
    }

    /// <summary>
    ///     Completes checkout for a guest stay and returns the room information required by other contexts.
    /// </summary>
    public async Task<GuestStayCheckoutSnapshot?> CompleteCheckout(
        string guestStayId,
        DateTime actualEndAt,
        CancellationToken cancellationToken = default)
    {
        var guestStay = await guestStayRepository.FindByIdAsync(guestStayId, cancellationToken);
        if (guestStay == null) return null;

        guestStay.Update(
            guestStay.HotelId,
            guestStay.RoomId,
            guestStay.GuestId,
            guestStay.GuestName,
            guestStay.StartAt,
            guestStay.ExpectedEndAt,
            actualEndAt,
            "finished",
            guestStay.BaseAmount,
            guestStay.AdditionalAmount,
            guestStay.PrepaidAmount,
            guestStay.TotalAmount,
            "paid");

        guestStayRepository.Update(guestStay);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new GuestStayCheckoutSnapshot(guestStay.Id, guestStay.HotelId, guestStay.RoomId);
    }
}
