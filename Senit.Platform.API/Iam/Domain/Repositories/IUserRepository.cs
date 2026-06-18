using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Iam.Domain.Repositories;

/// <summary>
///     Repository contract for user entities.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, string? excludedId = null, CancellationToken cancellationToken = default);
}
