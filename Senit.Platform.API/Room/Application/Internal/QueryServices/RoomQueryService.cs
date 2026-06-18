using Senit.Platform.API.Room.Application.QueryServices;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Domain.Model.Queries;
using Senit.Platform.API.Room.Domain.Repositories;

namespace Senit.Platform.API.Room.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for room use cases.
/// </summary>
public class RoomQueryService(IRoomRepository repository) : IRoomQueryService
{
    public async Task<IEnumerable<RoomEntity>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<RoomEntity?> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
