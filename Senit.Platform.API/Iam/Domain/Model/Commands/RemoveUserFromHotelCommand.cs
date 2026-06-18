namespace Senit.Platform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command used to remove a user assignment from a hotel without deleting the user account.
/// </summary>
/// <param name="HotelId">Hotel identifier.</param>
/// <param name="UserId">User identifier.</param>
public record RemoveUserFromHotelCommand(string HotelId, string UserId);
