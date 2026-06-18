using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.FrontDesk.Domain.Repositories;

/// <summary>
///     Repository contract for hotel entities.
/// </summary>
public interface IHotelRepository : IBaseRepository<Hotel>
{
    Task<bool> ExistsByRucAsync(string ruc, string? excludedId = null, CancellationToken cancellationToken = default);
}
