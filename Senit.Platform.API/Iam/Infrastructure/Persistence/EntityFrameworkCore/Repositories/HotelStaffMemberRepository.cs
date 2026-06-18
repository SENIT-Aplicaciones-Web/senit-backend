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
    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<HotelStaffMember?> FindFirstActiveAssignmentByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .FirstOrDefaultAsync(
                assignment => assignment.UserId == userId && assignment.Status == "active",
                cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> HasActiveAssignmentAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelStaffMember>()
            .AnyAsync(
                assignment => assignment.UserId == userId && assignment.Status == "active",
                cancellationToken);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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
