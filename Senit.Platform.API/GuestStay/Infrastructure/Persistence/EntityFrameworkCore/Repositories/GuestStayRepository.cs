using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for gueststay entities.
/// </summary>
public class GuestStayRepository(AppDbContext context) : BaseRepository<GuestStayRecord>(context), IGuestStayRepository
{
    public async Task<bool> ExistsActiveStayByRoomIdAsync(string roomId, string? excludedId = null, CancellationToken cancellationToken = default)
    {
        return await Context.Set<GuestStayRecord>()
            .AnyAsync(stay =>
                stay.RoomId == roomId &&
                stay.Status == "active" &&
                (excludedId == null || stay.Id != excludedId),
                cancellationToken);
    }

}
