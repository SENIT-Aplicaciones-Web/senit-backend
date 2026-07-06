using Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Extension methods for the custom request authorization middleware.
/// </summary>
public static class RequestAuthorizationMiddlewareExtensions
{
    /// <summary>
    ///     Registers RequestAuthorizationMiddleware in the ASP.NET Core pipeline.
    /// </summary>
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
