using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.GuestStay.Domain.Model.Aggregates;

/// <summary>
///     Represents a guest aggregate.
/// </summary>
public class Guest : AuditableEntity
{
    public string FullName { get; private set; } = string.Empty;
    public string Dni { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string? Email { get; private set; }

    public Guest()
    {
    }

    public Guest(
        string id,
        string fullName,
        string dni,
        string phone,
        string? email)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        FullName = fullName;
        Dni = dni;
        Phone = phone;
        Email = email;
    }

    public void Update(
        string fullName,
        string dni,
        string phone,
        string? email)
    {
        FullName = fullName;
        Dni = dni;
        Phone = phone;
        Email = email;
    }
}
