using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.Room.Interfaces.Acl;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Room.Application.Acl;

/// <summary>
///     Anti corruption facade implementation for the Room bounded context.
/// </summary>
public class RoomContextFacade(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork) : IRoomContextFacade
{
    /// <summary>
    ///     Finds a room and returns a snapshot that can be consumed by other contexts.
    /// </summary>
    public async Task<RoomSnapshot?> FindRoomById(string roomId, CancellationToken cancellationToken = default)
    {
        var room = await roomRepository.FindByIdAsync(roomId, cancellationToken);
        if (room == null) return null;

        return new RoomSnapshot(
            room.Id,
            room.HotelId,
            room.Number,
            room.Floor,
            room.Type,
            room.Capacity,
            room.PricePerHour,
            room.Status);
    }

    /// <summary>
    ///     Changes a room status through the Room bounded context.
    /// </summary>
    public async Task<bool> ChangeRoomStatus(string roomId, string status, CancellationToken cancellationToken = default)
    {
        var room = await roomRepository.FindByIdAsync(roomId, cancellationToken);
        if (room == null) return false;

        room.Update(
            room.HotelId,
            room.Number,
            room.Floor,
            room.Type,
            room.Capacity,
            room.PricePerHour,
            status);

        roomRepository.Update(room);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}
