using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for hotel staff assignments.
/// </summary>
public class HotelStaffMemberRepository(AppDbContext context)
    : BaseRepository<HotelStaffMember>(context), IHotelStaffMemberRepository
{
    /// <summary>
    ///     Finds a staff assignment by hotel and user identifiers.
    /// </summary>
    public async Task<HotelStaffMember?> FindByHotelIdAndUserIdAsync(
        string hotelId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .FirstOrDefaultAsync(
                assignment => assignment.HotelId == hotelId && assignment.UserId == userId,
                cancellationToken);
    }

    /// <summary>
    ///     Finds the first active hotel assignment for a user.
    /// </summary>
    public async Task<HotelStaffMember?> FindFirstActiveAssignmentByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .FirstOrDefaultAsync(
                assignment => assignment.UserId == userId && assignment.Status == "active",
                cancellationToken);
    }

    /// <summary>
    ///     Checks whether a user has any active hotel assignment.
    /// </summary>
    public async Task<bool> HasActiveAssignmentAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .AnyAsync(
                assignment => assignment.UserId == userId && assignment.Status == "active",
                cancellationToken);
    }

    /// <summary>
    ///     Checks whether a user has an active assignment in a specific hotel.
    /// </summary>
    public async Task<bool> HasActiveAssignmentAsync(
        string hotelId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .AnyAsync(
                assignment => assignment.HotelId == hotelId &&
                              assignment.UserId == userId &&
                              assignment.Status == "active",
                cancellationToken);
    }

    /// <summary>
    ///     Finds an active user assignment excluding a specific hotel.
    /// </summary>
    public async Task<HotelStaffMember?> FindFirstActiveAssignmentByUserIdExceptHotelAsync(
        string userId,
        string excludedHotelId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .FirstOrDefaultAsync(
                assignment => assignment.UserId == userId &&
                              assignment.HotelId != excludedHotelId &&
                              assignment.Status == "active",
                cancellationToken);
    }
}
