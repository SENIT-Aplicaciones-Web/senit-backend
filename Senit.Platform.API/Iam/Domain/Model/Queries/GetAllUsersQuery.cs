namespace Senit.Platform.API.Iam.Domain.Model.Queries;

/// <summary>
///     Query used to get users, optionally filtered by hotel assignment.
/// </summary>
/// <param name="HotelId">Hotel identifier used to return only active staff members of a hotel.</param>
public record GetAllUsersQuery(string? HotelId = null);
