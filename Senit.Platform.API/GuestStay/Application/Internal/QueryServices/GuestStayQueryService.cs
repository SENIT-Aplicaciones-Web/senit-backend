using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;
using Senit.Platform.API.GuestStay.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for gueststay use cases.
/// </summary>
public class GuestStayQueryService(IGuestStayRepository repository) : IGuestStayQueryService
{
    public async Task<IEnumerable<GuestStayRecord>> Handle(GetAllGuestStaysQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<GuestStayRecord?> Handle(GetGuestStayByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
