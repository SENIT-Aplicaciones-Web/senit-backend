namespace Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Marks an endpoint as public for the custom request authorization middleware.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}
