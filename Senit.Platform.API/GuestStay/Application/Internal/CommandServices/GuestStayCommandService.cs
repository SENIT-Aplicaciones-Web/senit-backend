using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.GuestStay.Domain.Model.Errors;
using Senit.Platform.API.Reservation.Interfaces.Acl;
using Senit.Platform.API.Room.Interfaces.Acl;

namespace Senit.Platform.API.GuestStay.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for gueststay use cases.
/// </summary>
public class GuestStayCommandService(
    IGuestStayRepository repository,
    IUnitOfWork unitOfWork,
    IRoomContextFacade roomContextFacade,
    IReservationContextFacade reservationContextFacade) : IGuestStayCommandService
{
    public async Task<ApplicationResult<GuestStayRecord>> Handle(CreateGuestStayCommand command, CancellationToken cancellationToken = default)
    {
        if (command.StartAt >= command.ExpectedEndAt)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        var duration = command.ExpectedEndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        var room = await roomContextFacade.FindRoomById(command.RoomId, cancellationToken);
        if (room == null)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.StayNotFound), StatusCodes.Status404NotFound);

        if (room.Status != "available")
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.RoomNotAvailable), StatusCodes.Status409Conflict);

        if (await repository.ExistsActiveStayByRoomIdAsync(command.RoomId, cancellationToken: cancellationToken))
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.RoomHasActiveStay), StatusCodes.Status409Conflict);

        if (await reservationContextFacade.HasOverlappingReservation(command.RoomId, command.StartAt, command.ExpectedEndAt, cancellationToken: cancellationToken))
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.ReservationOverlap), StatusCodes.Status409Conflict);
        var entity = new GuestStayRecord(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.RoomId,
            command.GuestId,
            command.GuestName,
            command.StartAt,
            command.ExpectedEndAt,
            command.ActualEndAt,
            command.Status,
            command.BaseAmount,
            command.AdditionalAmount,
            command.PrepaidAmount,
            command.TotalAmount,
            command.PaymentStatus);

        await repository.AddAsync(entity, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
        await roomContextFacade.ChangeRoomStatus(command.RoomId, "occupied", cancellationToken);

        return ApplicationResult<GuestStayRecord>.Created(entity);
    }

    public async Task<ApplicationResult<GuestStayRecord>> Handle(UpdateGuestStayCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.StayNotFound), StatusCodes.Status404NotFound);

        if (command.StartAt >= command.ExpectedEndAt)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        var duration = command.ExpectedEndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<GuestStayRecord>.Failure(nameof(GuestStayErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        entity.Update(
            command.HotelId,
            command.RoomId,
            command.GuestId,
            command.GuestName,
            command.StartAt,
            command.ExpectedEndAt,
            command.ActualEndAt,
            command.Status,
            command.BaseAmount,
            command.AdditionalAmount,
            command.PrepaidAmount,
            command.TotalAmount,
            command.PaymentStatus);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<GuestStayRecord>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteGuestStayCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(GuestStayErrors.StayNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
