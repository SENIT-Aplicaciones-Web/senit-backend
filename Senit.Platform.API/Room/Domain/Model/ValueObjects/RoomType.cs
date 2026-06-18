namespace Senit.Platform.API.Room.Domain.Model.ValueObjects;

/// <summary>
///     Provides the room types supported by the current web application.
/// </summary>
public static class RoomType
{
    private static readonly string[] AllowedValues = ["Standard", "Deluxe", "Suite"];

    /// <summary>
    ///     Gets a comma-separated list of allowed room types.
    /// </summary>
    public static string AllowedValuesDescription => string.Join(", ", AllowedValues);

    /// <summary>
    ///     Determines whether a room type is supported.
    /// </summary>
    /// <param name="type">The room type to validate.</param>
    /// <returns>True when the room type is supported; otherwise, false.</returns>
    public static bool IsAllowed(string type)
    {
        return AllowedValues.Any(value => string.Equals(value, type, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    ///     Returns the canonical room type value used by the frontend.
    /// </summary>
    /// <param name="type">The room type to normalize.</param>
    /// <returns>The canonical room type value.</returns>
    public static string Normalize(string type)
    {
        return AllowedValues.First(value => string.Equals(value, type, StringComparison.OrdinalIgnoreCase));
    }
}
