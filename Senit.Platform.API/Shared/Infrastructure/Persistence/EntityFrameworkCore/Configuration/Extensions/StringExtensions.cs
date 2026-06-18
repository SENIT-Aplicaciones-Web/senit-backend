using System.Text.RegularExpressions;

namespace Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     String extension methods for database naming conventions.
/// </summary>
public static partial class StringExtensions
{
    public static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        return SnakeCaseRegex().Replace(text, "_$1").ToLowerInvariant();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex SnakeCaseRegex();
}
