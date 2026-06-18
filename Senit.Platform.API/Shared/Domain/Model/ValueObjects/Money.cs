namespace Senit.Platform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Represents a positive monetary value used by business rules before persistence.
/// </summary>
/// <param name="Amount">The monetary amount.</param>
public readonly record struct Money(decimal Amount)
{
    public static bool IsPositive(decimal amount) => amount > 0;
}
