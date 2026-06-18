using Senit.Platform.API.FrontDesk.Application.QueryServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;
using Senit.Platform.API.FrontDesk.Domain.Repositories;

namespace Senit.Platform.API.FrontDesk.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for hotel use cases.
/// </summary>
public class HotelQueryService(IHotelRepository repository) : IHotelQueryService
{
    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Hotel?> Handle(GetHotelByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
