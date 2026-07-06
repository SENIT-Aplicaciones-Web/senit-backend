using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Iam.Application.CommandServices;

/// <summary>
///     Command service contract for authentication use cases.
/// </summary>
public interface IAuthenticationCommandService
{
    /// <summary>
    ///     Handles a sign in command.
    /// </summary>
    Task<ApplicationResult<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles a sign up command by creating the hotel and its administrator in one use case.
    /// </summary>
    Task<ApplicationResult<(User user, string token)>> Handle(SignUpCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles a password reset request from the public authentication flow.
    /// </summary>
    Task<ApplicationResult<bool>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken = default);
}
