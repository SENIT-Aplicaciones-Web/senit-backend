using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for consumption entities.
/// </summary>
public class ConsumptionRepository(AppDbContext context) : BaseRepository<Consumption>(context), IConsumptionRepository
{

}
