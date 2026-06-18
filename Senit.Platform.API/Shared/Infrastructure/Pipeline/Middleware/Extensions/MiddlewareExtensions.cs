using Senit.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace Senit.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Extension methods for registering middleware components.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    ///     Registers the global exception handler middleware.
    /// </summary>
    /// <param name="builder">
    ///     The application builder.
    /// </param>
    /// <returns>
    ///     The configured application builder.
    /// </returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
