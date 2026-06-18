using Senit.Platform.API.Reservation.Domain.Model.Queries;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Application.QueryServices;

/// <summary>
///     Query service contract for reservation use cases.
/// </summary>
public interface IReservationQueryService
{
    Task<IEnumerable<HotelReservation>> Handle(GetAllReservationsQuery query, CancellationToken cancellationToken = default);

    Task<HotelReservation?> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken = default);
}
