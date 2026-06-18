using System.Text.RegularExpressions;

namespace Senit.Platform.API.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

/// <summary>
///     String extension methods.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    ///     Converts a text to kebab case.
    /// </summary>
    /// <param name="text">
    ///     Text to convert.
    /// </param>
    /// <returns>
    ///     Kebab case text.
    /// </returns>
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        return KebabCaseRegex().Replace(text, "-$1")
            .Trim()
            .ToLowerInvariant();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}
