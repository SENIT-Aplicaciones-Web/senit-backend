using Senit.Platform.API.Reservation.Application.CommandServices;
using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Reservation.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Reservation.Domain.Model.Errors;
using Senit.Platform.API.Reservation.Domain.Model.ValueObjects;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for reservation use cases.
/// </summary>
public class ReservationCommandService(
    IReservationRepository repository,
    IUnitOfWork unitOfWork,
    IRoomRepository roomRepository,
    IGuestStayRepository guestStayRepository) : IReservationCommandService
{
    public async Task<ApplicationResult<HotelReservation>> Handle(CreateReservationCommand command, CancellationToken cancellationToken = default)
    {
        var requestedRange = new ReservationDateRange(command.StartAt, command.EndAt);
        if (!requestedRange.IsValid())
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        var duration = command.EndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        var room = await roomRepository.FindByIdAsync(command.RoomId, cancellationToken);
        if (room == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomNotFound), StatusCodes.Status404NotFound);

        if (room.Status == "maintenance")
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomInMaintenance), StatusCodes.Status409Conflict);

        if (command.GuestsQuantity < 1 || command.GuestsQuantity > room.Capacity)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidGuestsQuantity), StatusCodes.Status400BadRequest, room.Capacity);

        if (await repository.ExistsOverlappingReservationAsync(command.RoomId, command.StartAt, command.EndAt, cancellationToken: cancellationToken))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationOverlap), StatusCodes.Status409Conflict);

        if (await guestStayRepository.ExistsActiveStayByRoomIdAsync(command.RoomId, cancellationToken: cancellationToken))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationOverlap), StatusCodes.Status409Conflict);

        var hours = Convert.ToDecimal(duration.TotalHours);
        var reservationAmount = hours * room.PricePerHour;
        var entity = new HotelReservation(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.RoomId,
            command.GuestName,
            command.Dni,
            command.Phone,
            command.Email,
            command.GuestsQuantity,
            command.StartAt,
            command.EndAt,
            command.Status,
            hours,
            reservationAmount,
            command.PrepaidAmount,
            command.PaymentMethod,
            command.PaymentStatus,
            command.PaidAt);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<HotelReservation>.Created(entity);
    }

    public async Task<ApplicationResult<HotelReservation>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationNotFound), StatusCodes.Status404NotFound);

        var requestedRange = new ReservationDateRange(command.StartAt, command.EndAt);
        if (!requestedRange.IsValid())
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        var duration = command.EndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        var room = await roomRepository.FindByIdAsync(command.RoomId, cancellationToken);
        if (room == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomNotFound), StatusCodes.Status404NotFound);

        if (command.GuestsQuantity < 1 || command.GuestsQuantity > room.Capacity)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidGuestsQuantity), StatusCodes.Status400BadRequest, room.Capacity);

        if (await repository.ExistsOverlappingReservationAsync(command.RoomId, command.StartAt, command.EndAt, command.Id, cancellationToken))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationOverlap), StatusCodes.Status409Conflict);

        var hours = Convert.ToDecimal(duration.TotalHours);
        var reservationAmount = hours * room.PricePerHour;

        entity.Update(
            command.HotelId,
            command.RoomId,
            command.GuestName,
            command.Dni,
            command.Phone,
            command.Email,
            command.GuestsQuantity,
            command.StartAt,
            command.EndAt,
            command.Status,
            hours,
            reservationAmount,
            command.PrepaidAmount,
            command.PaymentMethod,
            command.PaymentStatus,
            command.PaidAt);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<HotelReservation>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteReservationCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(ReservationErrors.ReservationNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
