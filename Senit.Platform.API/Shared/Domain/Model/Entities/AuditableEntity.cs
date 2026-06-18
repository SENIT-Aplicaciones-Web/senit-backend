namespace Senit.Platform.API.Shared.Domain.Model.Entities;

/// <summary>
///     Base class for auditable entities.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public string Id { get; protected set; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
