using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Senit.Platform.API.Shared.Infrastructure.OpenApi;

/// <summary>
///     Adds realistic example values to Swagger schemas for API resources.
/// </summary>
public sealed class ResourceExampleSchemaFilter : ISchemaFilter
{
    /// <summary>
    ///     Builds a schema example from OpenApiExample attributes declared on resource properties.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being documented.</param>
    /// <param name="context">The schema generation context.</param>
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concrete) return;
        if (!IsRestResource(context.Type)) return;

        var example = BuildExample(context.Type);
        if (example.Count > 0)
            concrete.Example = example;
    }

    private static bool IsRestResource(Type type)
    {
        return type.Namespace?.Contains("Interfaces.Rest.Resources", StringComparison.Ordinal) == true &&
               type.Name.EndsWith("Resource", StringComparison.Ordinal);
    }

    private static JsonObject BuildExample(Type type)
    {
        var example = new JsonObject();

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var attribute = property.GetCustomAttribute<OpenApiExampleAttribute>();
            if (attribute is null) continue;

            var propertyName = JsonNamingPolicy.CamelCase.ConvertName(property.Name);
            example[propertyName] = ToJsonNode(attribute.Value);
        }

        return example;
    }

    private static JsonNode? ToJsonNode(object? value)
    {
        return value switch
        {
            null => null,
            string text => JsonValue.Create(text),
            int number => JsonValue.Create(number),
            long number => JsonValue.Create(number),
            decimal number => JsonValue.Create(number),
            double number => JsonValue.Create(number),
            float number => JsonValue.Create(number),
            bool boolean => JsonValue.Create(boolean),
            DateTime dateTime => JsonValue.Create(dateTime),
            _ => JsonValue.Create(value.ToString())
        };
    }
}
