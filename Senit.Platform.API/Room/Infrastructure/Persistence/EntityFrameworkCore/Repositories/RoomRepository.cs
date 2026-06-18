using Microsoft.EntityFrameworkCore;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Room.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for room entities.
/// </summary>
public class RoomRepository(AppDbContext context) : BaseRepository<RoomEntity>(context), IRoomRepository
{
    public async Task<bool> ExistsByHotelIdAndNumberAsync(string hotelId, string number, string? excludedId = null, CancellationToken cancellationToken = default)
    {
        return await Context.Set<RoomEntity>()
            .AnyAsync(room => room.HotelId == hotelId && room.Number == number && (excludedId == null || room.Id != excludedId), cancellationToken);
    }

}
