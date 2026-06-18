using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Housekeeping.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for cleaningtask entities.
/// </summary>
public class CleaningTaskRepository(AppDbContext context) : BaseRepository<CleaningTask>(context), ICleaningTaskRepository
{

}
