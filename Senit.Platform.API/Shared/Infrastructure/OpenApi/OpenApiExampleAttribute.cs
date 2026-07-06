namespace Senit.Platform.API.Shared.Infrastructure.OpenApi;

/// <summary>
///     Provides a sample value used only for OpenAPI documentation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class OpenApiExampleAttribute : Attribute
{
    /// <summary>
    ///     Initializes the attribute with a text sample value.
    /// </summary>
    /// <param name="value">The sample value shown in Swagger.</param>
    public OpenApiExampleAttribute(string? value)
    {
        Value = value;
    }

    /// <summary>
    ///     Initializes the attribute with an integer sample value.
    /// </summary>
    /// <param name="value">The sample value shown in Swagger.</param>
    public OpenApiExampleAttribute(int value)
    {
        Value = value;
    }

    /// <summary>
    ///     Initializes the attribute with a decimal sample value represented as a double.
    /// </summary>
    /// <param name="value">The sample value shown in Swagger.</param>
    public OpenApiExampleAttribute(double value)
    {
        Value = value;
    }

    /// <summary>
    ///     Initializes the attribute with a boolean sample value.
    /// </summary>
    /// <param name="value">The sample value shown in Swagger.</param>
    public OpenApiExampleAttribute(bool value)
    {
        Value = value;
    }

    /// <summary>
    ///     Gets the sample value shown in Swagger.
    /// </summary>
    public object? Value { get; }
}
