using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Senit.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Middleware that converts unhandled exceptions into Problem Details responses.
/// </summary>
public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    /// <summary>
    ///     Invokes the middleware for the current HTTP request.
    /// </summary>
    /// <param name="context">
    ///     The HTTP context.
    /// </param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception while processing request.");
            await WriteProblemDetailsAsync(context, exception);
        }
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Title = "InternalServerError",
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
