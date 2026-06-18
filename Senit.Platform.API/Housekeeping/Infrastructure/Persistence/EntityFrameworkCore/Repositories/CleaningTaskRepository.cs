using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Housekeeping.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for cleaning task entities.
/// </summary>
public class CleaningTaskRepository(AppDbContext context) : BaseRepository<CleaningTask>(context), ICleaningTaskRepository
{
    public async Task<IEnumerable<CleaningTask>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CleaningTask>()
            .Where(task => task.HotelId == hotelId)
            .ToListAsync(cancellationToken);
    }
}
