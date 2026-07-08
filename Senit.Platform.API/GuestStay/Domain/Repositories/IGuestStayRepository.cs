using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Domain.Repositories;

/// <summary>
///     Repository contract for gueststay entities.
/// </summary>
public interface IGuestStayRepository : IBaseRepository<GuestStayRecord>
{
    Task<IEnumerable<GuestStayRecord>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);
    Task<bool> ExistsActiveStayByRoomIdAsync(string roomId, string? excludedId = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsOverlappingActiveStayAsync(string roomId, DateTime startAt, DateTime endAt, string? excludedId = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsActiveStayByGuestDniAsync(string dni, string? excludedId = null, CancellationToken cancellationToken = default);
}
