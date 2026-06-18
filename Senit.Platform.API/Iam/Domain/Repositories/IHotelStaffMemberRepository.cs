using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Iam.Domain.Repositories;

/// <summary>
///     Repository contract for hotel staff assignments.
/// </summary>
public interface IHotelStaffMemberRepository : IBaseRepository<HotelStaffMember>
{
    /// <summary>
    ///     Finds a staff assignment by hotel and user.
    /// </summary>
    /// <param name="hotelId">Hotel identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The staff assignment when it exists; otherwise, null.</returns>
    Task<HotelStaffMember?> FindByHotelIdAndUserIdAsync(
        string hotelId,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds the first active hotel assignment for a user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The active staff assignment when it exists; otherwise, null.</returns>
    Task<HotelStaffMember?> FindFirstActiveAssignmentByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a user has at least one active staff assignment.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True when the user has an active assignment; otherwise, false.</returns>
    Task<bool> HasActiveAssignmentAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a user is active in a specific hotel.
    /// </summary>
    /// <param name="hotelId">Hotel identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True when the user is active in the hotel; otherwise, false.</returns>
    Task<bool> HasActiveAssignmentAsync(
        string hotelId,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds another active hotel assignment for a user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="excludedHotelId">Hotel identifier to exclude.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The active staff assignment when it exists; otherwise, null.</returns>
    Task<HotelStaffMember?> FindFirstActiveAssignmentByUserIdExceptHotelAsync(
        string userId,
        string excludedHotelId,
        CancellationToken cancellationToken = default);
}
