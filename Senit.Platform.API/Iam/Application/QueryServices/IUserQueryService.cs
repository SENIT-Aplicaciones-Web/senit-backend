using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Queries;

namespace Senit.Platform.API.Iam.Application.QueryServices;

/// <summary>
///     Query service contract for user use cases.
/// </summary>
public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default);

    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default);
}
