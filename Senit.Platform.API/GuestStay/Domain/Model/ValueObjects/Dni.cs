namespace Senit.Platform.API.GuestStay.Domain.Model.ValueObjects;

/// <summary>
///     Represents a Peruvian DNI number.
/// </summary>
/// <param name="Value">The DNI value.</param>
public readonly record struct Dni(string Value)
{
    public static bool IsValid(string value) => !string.IsNullOrWhiteSpace(value) && value.Length == 8 && value.All(char.IsDigit);
}
