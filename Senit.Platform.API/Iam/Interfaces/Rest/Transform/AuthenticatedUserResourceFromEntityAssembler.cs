using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts an authenticated user entity into a REST resource.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "User cannot be null when converting to resource.");

        return new AuthenticatedUserResource(
            entity.Id,
            entity.HotelId,
            entity.FullName,
            entity.Username,
            entity.Email,
            entity.Role,
            entity.Status);
    }
}
