namespace Senit.Platform.API.GuestStay.Interfaces.Acl;

/// <summary>
///     Guest stay data exposed through the anti corruption layer without leaking GuestStay aggregates.
/// </summary>
public sealed record GuestStayCheckoutSnapshot(string Id, string HotelId, string RoomId);

/// <summary>
///     Anti corruption facade exposed by the GuestStay bounded context.
/// </summary>
public interface IGuestStayContextFacade
{
    /// <summary>
    ///     Checks whether a room has an active stay without exposing GuestStay repositories.
    /// </summary>
    Task<bool> HasActiveStayByRoomId(
        string roomId,
        string? excludedStayId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a room has an active stay that overlaps a requested period.
    /// </summary>
    Task<bool> HasOverlappingActiveStay(
        string roomId,
        DateTime startAt,
        DateTime endAt,
        string? excludedStayId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a guest stay as finished and paid, returning the data needed by checkout orchestration.
    /// </summary>
    Task<GuestStayCheckoutSnapshot?> CompleteCheckout(
        string guestStayId,
        DateTime actualEndAt,
        CancellationToken cancellationToken = default);
}
