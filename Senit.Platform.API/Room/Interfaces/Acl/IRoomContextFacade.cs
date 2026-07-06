namespace Senit.Platform.API.Room.Interfaces.Acl;

/// <summary>
///     Room data exposed through the anti corruption layer without leaking Room aggregates.
/// </summary>
public sealed record RoomSnapshot(
    string Id,
    string HotelId,
    string Number,
    int Floor,
    string Type,
    int Capacity,
    decimal PricePerHour,
    string Status);

/// <summary>
///     Anti corruption facade exposed by the Room bounded context.
/// </summary>
public interface IRoomContextFacade
{
    /// <summary>
    ///     Finds room data without exposing the Room aggregate.
    /// </summary>
    Task<RoomSnapshot?> FindRoomById(string roomId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Changes a room status from another bounded context use case.
    /// </summary>
    Task<bool> ChangeRoomStatus(string roomId, string status, CancellationToken cancellationToken = default);
}
