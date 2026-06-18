namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to create a user.
/// </summary>
public record CreateUserCommand(
    string HotelId,
    string FullName,
    string Username,
    string Email,
    string Password,
    string Role,
    string Status);
