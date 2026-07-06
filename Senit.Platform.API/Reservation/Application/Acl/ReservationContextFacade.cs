using Senit.Platform.API.Reservation.Domain.Repositories;
using Senit.Platform.API.Reservation.Interfaces.Acl;

namespace Senit.Platform.API.Reservation.Application.Acl;

/// <summary>
///     Anti corruption facade implementation for the Reservation bounded context.
/// </summary>
public class ReservationContextFacade(IReservationRepository reservationRepository) : IReservationContextFacade
{
    /// <summary>
    ///     Checks whether a room has an overlapping reservation in the requested time range.
    /// </summary>
    public async Task<bool> HasOverlappingReservation(
        string roomId,
        DateTime startAt,
        DateTime endAt,
        string? excludedReservationId = null,
        CancellationToken cancellationToken = default)
    {
        return await reservationRepository.ExistsOverlappingReservationAsync(
            roomId,
            startAt,
            endAt,
            excludedReservationId,
            cancellationToken);
    }
}
