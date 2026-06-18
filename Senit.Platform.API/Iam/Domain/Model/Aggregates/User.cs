using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Iam.Domain.Model.Aggregates;

/// <summary>
///     Represents a user aggregate.
/// </summary>
public class User : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;

    public User()
    {
    }

    public User(
        string id,
        string hotelId,
        string fullName,
        string username,
        string email,
        string password,
        string role,
        string status)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        FullName = fullName;
        Username = username;
        Email = email;
        Password = password;
        Role = role;
        Status = status;
    }

    public void Update(
        string hotelId,
        string fullName,
        string username,
        string email,
        string password,
        string role,
        string status)
    {
        HotelId = hotelId;
        FullName = fullName;
        Username = username;
        Email = email;
        Password = password;
        Role = role;
        Status = status;
    }

    /// <summary>
    ///     Changes the default hotel used by the frontend session.
    /// </summary>
    /// <param name="hotelId">Default hotel identifier.</param>
    public void ChangeDefaultHotel(string hotelId)
    {
        HotelId = hotelId;
    }

    /// <summary>
    ///     Updates the user role.
    /// </summary>
    /// <param name="role">New role.</param>
    public void ChangeRole(string role)
    {
        Role = role;
    }

    /// <summary>
    ///     Marks the user account as active.
    /// </summary>
    public void Activate()
    {
        Status = "active";
    }

    /// <summary>
    ///     Marks the user account as inactive without deleting it.
    /// </summary>
    public void Deactivate()
    {
        Status = "inactive";
    }

}
