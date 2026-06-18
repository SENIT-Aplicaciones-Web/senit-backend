using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Application.CommandServices;

/// <summary>
///     Command service contract for reservation use cases.
/// </summary>
public interface IReservationCommandService
{
    Task<ApplicationResult<HotelReservation>> Handle(CreateReservationCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<HotelReservation>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteReservationCommand command, CancellationToken cancellationToken = default);
}
