using System.Net.Mail;
using System.Text.RegularExpressions;
using Senit.Platform.API.Reservation.Application.CommandServices;
using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Reservation.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.GuestStay.Interfaces.Acl;
using Senit.Platform.API.Room.Interfaces.Acl;
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
    IRoomContextFacade roomContextFacade,
    IGuestStayContextFacade guestStayContextFacade) : IReservationCommandService
{
    public async Task<ApplicationResult<HotelReservation>> Handle(CreateReservationCommand command, CancellationToken cancellationToken = default)
    {
        var fieldValidation = ValidateReservationFields(
            command.HotelId,
            command.RoomId,
            command.GuestName,
            command.Dni,
            command.Phone,
            command.Email,
            command.GuestsQuantity,
            command.Status,
            command.PrepaidAmount,
            command.PaymentMethod);

        if (fieldValidation.ErrorCode is not null)
            return ApplicationResult<HotelReservation>.Failure(fieldValidation.ErrorCode, StatusCodes.Status400BadRequest, fieldValidation.Arguments);

        var requestedRange = new ReservationDateRange(command.StartAt, command.EndAt);
        if (!requestedRange.IsValid())
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        if (command.StartAt < DateTime.UtcNow.AddMinutes(-5))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.StartDateInPast), StatusCodes.Status400BadRequest);

        var duration = command.EndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        var room = await roomContextFacade.FindRoomById(command.RoomId, cancellationToken);
        if (room == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomNotFound), StatusCodes.Status404NotFound);

        if (room.Status == "maintenance")
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomInMaintenance), StatusCodes.Status409Conflict);

        if (command.GuestsQuantity < 1 || command.GuestsQuantity > room.Capacity)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidGuestsQuantity), StatusCodes.Status400BadRequest, room.Capacity);

        if (await repository.ExistsOverlappingReservationAsync(command.RoomId, command.StartAt, command.EndAt, cancellationToken: cancellationToken))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationOverlap), StatusCodes.Status409Conflict);

        if (await guestStayContextFacade.HasOverlappingActiveStay(command.RoomId, command.StartAt, command.EndAt, cancellationToken: cancellationToken))
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
            command.AdditionalGuestsJson,
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
        var fieldValidation = ValidateReservationFields(
            command.HotelId,
            command.RoomId,
            command.GuestName,
            command.Dni,
            command.Phone,
            command.Email,
            command.GuestsQuantity,
            command.Status,
            command.PrepaidAmount,
            command.PaymentMethod);

        if (fieldValidation.ErrorCode is not null)
            return ApplicationResult<HotelReservation>.Failure(fieldValidation.ErrorCode, StatusCodes.Status400BadRequest, fieldValidation.Arguments);

        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationNotFound), StatusCodes.Status404NotFound);

        if (command.Status.Equals("cancelled", StringComparison.OrdinalIgnoreCase))
        {
            if (DateTime.UtcNow >= entity.StartAt)
                return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.CannotCancelStartedReservation), StatusCodes.Status409Conflict);

            entity.Update(
                entity.HotelId,
                entity.RoomId,
                entity.GuestName,
                entity.Dni,
                entity.Phone,
                entity.Email,
                entity.GuestsQuantity,
                entity.AdditionalGuestsJson,
                entity.StartAt,
                entity.EndAt,
                "cancelled",
                entity.Hours,
                entity.ReservationAmount,
                entity.PrepaidAmount,
                entity.PaymentMethod,
                entity.PaymentStatus,
                entity.PaidAt);

            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);

            return ApplicationResult<HotelReservation>.Success(entity);
        }

        var requestedRange = new ReservationDateRange(command.StartAt, command.EndAt);
        if (!requestedRange.IsValid())
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDateRange), StatusCodes.Status400BadRequest);

        if (command.StartAt < DateTime.UtcNow.AddMinutes(-5))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.StartDateInPast), StatusCodes.Status400BadRequest);

        var duration = command.EndAt - command.StartAt;
        if (duration.Ticks % TimeSpan.FromHours(1).Ticks != 0)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidDurationHours), StatusCodes.Status400BadRequest);

        var room = await roomContextFacade.FindRoomById(command.RoomId, cancellationToken);
        if (room == null)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomNotFound), StatusCodes.Status404NotFound);

        if (room.Status == "maintenance")
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.RoomInMaintenance), StatusCodes.Status409Conflict);

        if (command.GuestsQuantity < 1 || command.GuestsQuantity > room.Capacity)
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.InvalidGuestsQuantity), StatusCodes.Status400BadRequest, room.Capacity);

        if (await repository.ExistsOverlappingReservationAsync(command.RoomId, command.StartAt, command.EndAt, command.Id, cancellationToken))
            return ApplicationResult<HotelReservation>.Failure(nameof(ReservationErrors.ReservationOverlap), StatusCodes.Status409Conflict);

        if (await guestStayContextFacade.HasOverlappingActiveStay(command.RoomId, command.StartAt, command.EndAt, cancellationToken: cancellationToken))
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
            command.AdditionalGuestsJson,
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

    private static (string? ErrorCode, object[] Arguments) ValidateReservationFields(
        string hotelId,
        string roomId,
        string guestName,
        string dni,
        string phone,
        string? email,
        int guestsQuantity,
        string status,
        decimal prepaidAmount,
        string paymentMethod)
    {
        if (string.IsNullOrWhiteSpace(hotelId) || string.IsNullOrWhiteSpace(roomId))
            return (nameof(ReservationErrors.InvalidRequest), Array.Empty<object>());

        if (!Regex.IsMatch(guestName.Trim(), @"^[\p{L}\s.'-]{3,80}$"))
            return (nameof(ReservationErrors.InvalidGuestName), Array.Empty<object>());

        if (!Regex.IsMatch(dni.Trim(), @"^\d{8}$"))
            return (nameof(ReservationErrors.InvalidDni), Array.Empty<object>());

        if (!Regex.IsMatch(phone.Trim(), @"^\d{9}$"))
            return (nameof(ReservationErrors.InvalidPhone), Array.Empty<object>());

        if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email.Trim()))
            return (nameof(ReservationErrors.InvalidEmail), Array.Empty<object>());

        if (guestsQuantity < 1)
            return (nameof(ReservationErrors.InvalidGuestsQuantity), Array.Empty<object>());

        var allowedStatuses = new[] { "confirmed", "cancelled", "completed" };
        if (!allowedStatuses.Contains(status.Trim(), StringComparer.OrdinalIgnoreCase))
            return (nameof(ReservationErrors.InvalidReservationStatus), Array.Empty<object>());

        var allowedPaymentMethods = new[] { "cash", "card", "transfer", "yape" };
        if (!allowedPaymentMethods.Contains(paymentMethod.Trim(), StringComparer.OrdinalIgnoreCase))
            return (nameof(ReservationErrors.InvalidPaymentMethod), Array.Empty<object>());

        if (prepaidAmount < 0)
            return (nameof(ReservationErrors.InvalidPrepaidAmount), Array.Empty<object>());

        return (null, Array.Empty<object>());
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var address = new MailAddress(email);
            return string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

}
