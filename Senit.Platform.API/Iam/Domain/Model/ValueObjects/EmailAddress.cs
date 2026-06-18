namespace Senit.Platform.API.Iam.Domain.Model.ValueObjects;

/// <summary>
///     Represents an email address used for sign-in and user registration.
/// </summary>
/// <param name="Value">The normalized email value.</param>
public readonly record struct EmailAddress(string Value)
{
    public static string Normalize(string value) => value.Trim().ToLowerInvariant();
}
