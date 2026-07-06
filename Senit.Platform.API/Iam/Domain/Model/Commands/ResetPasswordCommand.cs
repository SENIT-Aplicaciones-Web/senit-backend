namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to reset a user password by email from the authentication flow.
/// </summary>
public record ResetPasswordCommand(string Email, string NewPassword);
