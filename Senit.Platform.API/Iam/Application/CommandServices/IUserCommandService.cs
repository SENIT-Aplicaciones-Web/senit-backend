using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Iam.Application.CommandServices;

/// <summary>
///     Command service contract for user use cases.
/// </summary>
public interface IUserCommandService
{
    Task<ApplicationResult<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(RemoveUserFromHotelCommand command, CancellationToken cancellationToken = default);
}
