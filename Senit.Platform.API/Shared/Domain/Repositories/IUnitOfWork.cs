namespace Senit.Platform.API.Shared.Domain.Repositories;

/// <summary>
///     Unit of work contract.
/// </summary>
public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken = default);
}
