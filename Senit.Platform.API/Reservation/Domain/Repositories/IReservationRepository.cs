using Senit.Platform.API.Shared.Domain.Repositories;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Domain.Repositories;

/// <summary>
///     Repository contract for reservation entities.
/// </summary>
public interface IReservationRepository : IBaseRepository<HotelReservation>
{
    Task<bool> ExistsOverlappingReservationAsync(string roomId, DateTime startAt, DateTime endAt, string? excludedId = null, CancellationToken cancellationToken = default);
}
