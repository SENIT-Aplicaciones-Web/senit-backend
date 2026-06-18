namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to register a new hotel administrator.
/// </summary>
/// <param name="Username">
///     The administrator username.
/// </param>
/// <param name="Email">
///     The administrator email address.
/// </param>
/// <param name="Password">
///     The administrator password.
/// </param>
public record SignUpCommand(
    string Username,
    string Email,
    string Password);
