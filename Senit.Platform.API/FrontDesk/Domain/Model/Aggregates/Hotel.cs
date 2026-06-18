using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;

/// <summary>
///     Represents a hotel aggregate.
/// </summary>
public class Hotel : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Ruc { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Plan { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;

    public Hotel()
    {
    }

    public Hotel(
        string id,
        string name,
        string ruc,
        string address,
        string phone,
        string email,
        string plan,
        string status)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        Name = name;
        Ruc = ruc;
        Address = address;
        Phone = phone;
        Email = email;
        Plan = plan;
        Status = status;
    }

    public void Update(
        string name,
        string ruc,
        string address,
        string phone,
        string email,
        string plan,
        string status)
    {
        Name = name;
        Ruc = ruc;
        Address = address;
        Phone = phone;
        Email = email;
        Plan = plan;
        Status = status;
    }
}
