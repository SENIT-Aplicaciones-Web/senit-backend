using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Converts a user entity to a resource.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        if (entity == null)
            throw new ArgumentNullException(
                nameof(entity),
                "User cannot be null when converting to resource.");

        return new UserResource(
            entity.Id,
            entity.HotelId,
            entity.FullName,
            entity.Username,
            entity.Email,
            entity.Password,
            entity.Role,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
