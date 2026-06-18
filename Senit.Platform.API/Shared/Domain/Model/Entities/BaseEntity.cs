namespace Senit.Platform.API.Shared.Domain.Model.Entities;

/// <summary>
///     Base entity for resources with string identifiers compatible with the current frontend.
/// </summary>
public abstract class BaseEntity : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
