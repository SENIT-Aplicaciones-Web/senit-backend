using Senit.Platform.API.Room.Application.CommandServices;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Domain.Model.Commands;
using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.Room.Domain.Model.Errors;
using Senit.Platform.API.Room.Domain.Model.ValueObjects;

namespace Senit.Platform.API.Room.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for room use cases.
/// </summary>
public class RoomCommandService(
    IRoomRepository repository,
    IUnitOfWork unitOfWork) : IRoomCommandService
{
    public async Task<ApplicationResult<RoomEntity>> Handle(CreateRoomCommand command, CancellationToken cancellationToken = default)
    {
        var roomNumber = new RoomNumber(command.Number);
        if (await repository.ExistsByHotelIdAndNumberAsync(command.HotelId, roomNumber.Value, cancellationToken: cancellationToken))
            return ApplicationResult<RoomEntity>.Failure(nameof(RoomErrors.DuplicateRoomNumber), StatusCodes.Status409Conflict, command.Number);

        if (!RoomType.IsAllowed(command.Type))
            return ApplicationResult<RoomEntity>.Failure(
                nameof(RoomErrors.InvalidRoomType),
                StatusCodes.Status400BadRequest,
                RoomType.AllowedValuesDescription);

        if (!RoomStatus.IsAllowedForManualChange(command.Status))
            return ApplicationResult<RoomEntity>.Failure(
                nameof(RoomErrors.InvalidStatus),
                StatusCodes.Status400BadRequest,
                RoomStatus.ManualValuesDescription);

        var entity = new RoomEntity(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.Number,
            command.Floor,
            RoomType.Normalize(command.Type),
            command.Capacity,
            command.PricePerHour,
            RoomStatus.NormalizeManualStatus(command.Status));

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<RoomEntity>.Created(entity);
    }

    public async Task<ApplicationResult<RoomEntity>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<RoomEntity>.Failure(nameof(RoomErrors.RoomNotFound), StatusCodes.Status404NotFound);

        var roomNumber = new RoomNumber(command.Number);
        if (await repository.ExistsByHotelIdAndNumberAsync(command.HotelId, roomNumber.Value, command.Id, cancellationToken))
            return ApplicationResult<RoomEntity>.Failure(nameof(RoomErrors.DuplicateRoomNumber), StatusCodes.Status409Conflict, command.Number);

        if (!RoomType.IsAllowed(command.Type))
            return ApplicationResult<RoomEntity>.Failure(
                nameof(RoomErrors.InvalidRoomType),
                StatusCodes.Status400BadRequest,
                RoomType.AllowedValuesDescription);

        if (!RoomStatus.IsAllowedForManualChange(command.Status))
            return ApplicationResult<RoomEntity>.Failure(
                nameof(RoomErrors.InvalidStatus),
                StatusCodes.Status400BadRequest,
                RoomStatus.ManualValuesDescription);

        entity.Update(
            command.HotelId,
            command.Number,
            command.Floor,
            RoomType.Normalize(command.Type),
            command.Capacity,
            command.PricePerHour,
            RoomStatus.NormalizeManualStatus(command.Status));

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<RoomEntity>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(RoomErrors.RoomNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
