using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Room.Application.CommandServices;

/// <summary>
///     Command service contract for room use cases.
/// </summary>
public interface IRoomCommandService
{
    Task<ApplicationResult<RoomEntity>> Handle(CreateRoomCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<RoomEntity>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken = default);
}
