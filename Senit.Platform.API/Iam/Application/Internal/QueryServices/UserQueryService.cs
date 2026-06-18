using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Queries;
using Senit.Platform.API.Iam.Domain.Repositories;

namespace Senit.Platform.API.Iam.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for user use cases.
/// </summary>
public class UserQueryService(
    IUserRepository repository,
    IHotelStaffMemberRepository hotelStaffMemberRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = (await repository.ListAsync(cancellationToken)).ToList();

        if (!string.IsNullOrWhiteSpace(query.HotelId))
        {
            var hotelUsers = new List<User>();

            foreach (var user in users)
            {
                var assignment = await hotelStaffMemberRepository.FindByHotelIdAndUserIdAsync(
                    query.HotelId,
                    user.Id,
                    cancellationToken);

                if (assignment is null || assignment.Status != "active")
                    continue;

                user.ChangeDefaultHotel(assignment.HotelId);
                user.ChangeRole(assignment.Role);
                hotelUsers.Add(user);
            }

            return hotelUsers;
        }

        foreach (var user in users)
            await ApplyActiveAssignmentAsync(user, cancellationToken);

        return users;
    }

    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var user = await repository.FindByIdAsync(query.Id, cancellationToken);

        if (user is null)
            return null;

        await ApplyActiveAssignmentAsync(user, cancellationToken);
        return user;
    }

    private async Task ApplyActiveAssignmentAsync(User user, CancellationToken cancellationToken)
    {
        var assignment = await hotelStaffMemberRepository.FindFirstActiveAssignmentByUserIdAsync(
            user.Id,
            cancellationToken);

        if (assignment is null)
            return;

        user.ChangeDefaultHotel(assignment.HotelId);
        user.ChangeRole(assignment.Role);
    }
}
