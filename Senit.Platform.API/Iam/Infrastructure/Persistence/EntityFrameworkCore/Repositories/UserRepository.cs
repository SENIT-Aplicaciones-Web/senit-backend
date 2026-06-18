using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for user entities.
/// </summary>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, string? excludedId = null, CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>()
            .AnyAsync(user => user.Email == email && (excludedId == null || user.Id != excludedId), cancellationToken);
    }

}
