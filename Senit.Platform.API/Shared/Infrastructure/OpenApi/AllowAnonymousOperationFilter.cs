using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace Senit.Platform.API.Shared.Infrastructure.OpenApi;

/// <summary>
///     Configures Swagger authorization metadata for public and protected endpoints.
/// </summary>
public sealed class AllowAnonymousOperationFilter : IOperationFilter
{
    private const string UnauthorizedDescription = "Unauthorized missing expired or invalid JWT bearer token";
    private const string BadRequestDescription = "Bad request invalid input values or unsupported allowed values";

    /// <summary>
    ///     Removes bearer security from public endpoints and documents common protected endpoint responses.
    /// </summary>
    /// <param name="operation">The Swagger operation being documented.</param>
    /// <param name="context">The current Swagger operation filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodAllowsAnonymous = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any();

        var controllerAllowsAnonymous = context.MethodInfo.DeclaringType?
            .GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any() ?? false;

        if (methodAllowsAnonymous || controllerAllowsAnonymous)
        {
            operation.Security ??= [];
            operation.Security.Clear();
        }
        else
        {
            operation.Responses["401"] = new OpenApiResponse
            {
                Description = UnauthorizedDescription
            };
        }

        if (operation.RequestBody is not null)
        {
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = BadRequestDescription
            };
        }
    }
}
