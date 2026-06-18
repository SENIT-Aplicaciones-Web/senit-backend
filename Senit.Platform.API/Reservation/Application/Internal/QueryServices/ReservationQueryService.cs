using Senit.Platform.API.Reservation.Application.QueryServices;
using Senit.Platform.API.Reservation.Domain.Model.Queries;
using Senit.Platform.API.Reservation.Domain.Repositories;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for reservation use cases.
/// </summary>
public class ReservationQueryService(IReservationRepository repository) : IReservationQueryService
{
    public async Task<IEnumerable<HotelReservation>> Handle(GetAllReservationsQuery query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(query.HotelId))
            return await repository.ListByHotelIdAsync(query.HotelId, cancellationToken);

        return await repository.ListAsync(cancellationToken);
    }

    public async Task<HotelReservation?> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
