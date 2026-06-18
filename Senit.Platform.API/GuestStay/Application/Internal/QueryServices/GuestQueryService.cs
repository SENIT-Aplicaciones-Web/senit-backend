using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;
using Senit.Platform.API.GuestStay.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for guest use cases.
/// </summary>
public class GuestQueryService(IGuestRepository repository) : IGuestQueryService
{
    public async Task<IEnumerable<Guest>> Handle(GetAllGuestsQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Guest?> Handle(GetGuestByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
