namespace Senit.Platform.API.Shared.Domain.Model.Entities;

/// <summary>
///     Contract for entities that keep audit timestamps.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
