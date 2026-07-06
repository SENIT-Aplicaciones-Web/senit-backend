using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Senit.Platform.API.Iam.Application.Internal.OutboundServices;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Infrastructure.Tokens.Jwt.Configuration;

namespace Senit.Platform.API.Iam.Infrastructure.Tokens.Jwt.Services;

/// <summary>
///     The token service.
/// </summary>
/// <remarks>
///     This class is used to generate and validate JWT tokens.
/// </remarks>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    /// <summary>
    ///     Generate token.
    /// </summary>
    /// <param name="user">The user for token generation.</param>
    /// <returns>The created token.</returns>
    public string GenerateToken(User user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    /// <summary>
    ///     Verify token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>The user id if the token is valid, otherwise null.</returns>
    public async Task<string?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            });

            if (!tokenValidationResult.IsValid) return null;

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            return jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
