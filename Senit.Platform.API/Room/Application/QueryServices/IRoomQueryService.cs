using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Domain.Model.Queries;

namespace Senit.Platform.API.Room.Application.QueryServices;

/// <summary>
///     Query service contract for room use cases.
/// </summary>
public interface IRoomQueryService
{
    Task<IEnumerable<RoomEntity>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken = default);

    Task<RoomEntity?> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken = default);
}
