namespace Senit.Platform.API.Iam.Domain.Model.Errors;

/// <summary>
///     Iam context error codes used by application services.
/// </summary>
public enum IamErrors
{
    InvalidCredentials,
    InvalidRequest,
    DuplicateEmail,
    UserNotFound,
    UserNotAssignedToHotel,
    HotelNotFound,
    InvalidPassword
}
