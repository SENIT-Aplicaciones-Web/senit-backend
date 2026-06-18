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
    ///     Handles a sign-in command.
    /// </summary>
    /// <param name="command">
    ///     The sign-in command.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     The authenticated user when credentials are valid.
    /// </returns>
    Task<ApplicationResult<User>> Handle(SignInCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles a sign-up command by creating the hotel and its administrator in one use case.
    /// </summary>
    /// <param name="command">
    ///     The sign-up command.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     The created administrator user.
    /// </returns>
    Task<ApplicationResult<User>> Handle(SignUpCommand command, CancellationToken cancellationToken = default);
}
