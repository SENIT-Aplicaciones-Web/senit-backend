using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts IAM users and JWT tokens into REST resources.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "User cannot be null when converting to resource.");

        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty when converting to resource.", nameof(token));

        return new AuthenticatedUserResource(
            entity.Id,
            entity.HotelId,
            entity.FullName,
            entity.Username,
            entity.Email,
            entity.Role,
            entity.Status,
            token);
    }
}
