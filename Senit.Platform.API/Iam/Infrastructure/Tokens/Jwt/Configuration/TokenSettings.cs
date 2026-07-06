namespace Senit.Platform.API.Iam.Infrastructure.Tokens.Jwt.Configuration;

/// <summary>
///     This class is used to store the JWT token settings.
///     Keep the Secret value in deployment environment variables, not in source control.
/// </summary>
public class TokenSettings
{
    public required string Secret { get; set; }
}
