namespace Senit.Platform.API.Reservation.Interfaces.Acl;

/// <summary>
///     Anti corruption facade exposed by the Reservation bounded context.
/// </summary>
public interface IReservationContextFacade
{
    /// <summary>
    ///     Checks reservation overlap without exposing Reservation repositories or aggregates.
    /// </summary>
    Task<bool> HasOverlappingReservation(
        string roomId,
        DateTime startAt,
        DateTime endAt,
        string? excludedReservationId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a room has any confirmed reservation.
    /// </summary>
    Task<bool> HasConfirmedReservationByRoomId(
        string roomId,
        CancellationToken cancellationToken = default);
}
