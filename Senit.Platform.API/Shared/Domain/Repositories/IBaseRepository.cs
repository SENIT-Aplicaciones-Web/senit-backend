namespace Senit.Platform.API.Shared.Domain.Repositories;

/// <summary>
///     Common repository contract.
/// </summary>
/// <typeparam name="TEntity">
///     Entity type.
/// </typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> FindByIdAsync(string id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
