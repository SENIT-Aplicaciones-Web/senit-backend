namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to sign in a user.
/// </summary>
public record SignInCommand(string Email, string Password);
