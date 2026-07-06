namespace Senit.Platform.API.Room.Domain.Model.ValueObjects;

/// <summary>
///     Provides room statuses supported by direct room administration.
/// </summary>
public static class RoomStatus
{
    private static readonly string[] ManualValues = ["available", "cleaning", "maintenance"];

    /// <summary>
    ///     Gets a comma separated list of statuses that can be assigned directly from room management.
    /// </summary>
    public static string ManualValuesDescription => string.Join(", ", ManualValues);

    /// <summary>
    ///     Determines whether a room status can be assigned directly by an administrator or receptionist.
    /// </summary>
    /// <param name="status">The status to validate.</param>
    /// <returns>True when the status can be assigned manually, otherwise false.</returns>
    public static bool IsAllowedForManualChange(string status)
    {
        return ManualValues.Any(value => string.Equals(value, status, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    ///     Returns the canonical status value used by the frontend.
    /// </summary>
    /// <param name="status">The status to normalize.</param>
    /// <returns>The canonical status value.</returns>
    public static string NormalizeManualStatus(string status)
    {
        return ManualValues.First(value => string.Equals(value, status, StringComparison.OrdinalIgnoreCase));
    }
}
