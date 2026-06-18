namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to update a user.
/// </summary>
public record UpdateUserCommand(
    string Id,
    string HotelId,
    string FullName,
    string Username,
    string Email,
    string Password,
    string Role,
    string Status);
