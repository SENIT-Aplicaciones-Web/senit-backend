using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.FrontDesk.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for hotel entities.
/// </summary>
public class HotelRepository(AppDbContext context) : BaseRepository<Hotel>(context), IHotelRepository
{
    public async Task<bool> ExistsByRucAsync(string ruc, string? excludedId = null, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Hotel>()
            .AnyAsync(hotel => hotel.Ruc == ruc && (excludedId == null || hotel.Id != excludedId), cancellationToken);
    }

}
