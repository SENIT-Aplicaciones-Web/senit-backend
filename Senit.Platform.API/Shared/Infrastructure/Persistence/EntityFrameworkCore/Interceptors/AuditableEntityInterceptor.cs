using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

/// <summary>
///     Interceptor that sets audit timestamps for auditable entities.
/// </summary>
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            var utcNow = DateTime.UtcNow;
            foreach (var entry in eventData.Context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = utcNow;
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = utcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
