using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Domain.Repositories;

/// <summary>
///     Repository contract for gueststay entities.
/// </summary>
public interface IGuestStayRepository : IBaseRepository<GuestStayRecord>
{
    Task<bool> ExistsActiveStayByRoomIdAsync(string roomId, string? excludedId = null, CancellationToken cancellationToken = default);
}
