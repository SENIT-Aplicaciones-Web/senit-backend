using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Iam.Domain.Model.Aggregates;

/// <summary>
///     Represents the assignment of a user to a hotel staff list.
/// </summary>
public class HotelStaffMember : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;

    public HotelStaffMember()
    {
    }

    public HotelStaffMember(
        string id,
        string hotelId,
        string userId,
        string role,
        string status)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        UserId = userId;
        Role = role;
        Status = status;
    }

    /// <summary>
    ///     Updates the staff role and status inside the hotel.
    /// </summary>
    /// <param name="role">Role assigned to the user inside the hotel.</param>
    /// <param name="status">Assignment status.</param>
    public void Update(string role, string status)
    {
        Role = role;
        Status = status;
    }

    /// <summary>
    ///     Marks the staff assignment as inactive without deleting the user account.
    /// </summary>
    public void Deactivate()
    {
        Status = "inactive";
    }

    /// <summary>
    ///     Marks the staff assignment as active.
    /// </summary>
    public void Activate()
    {
        Status = "active";
    }
}
