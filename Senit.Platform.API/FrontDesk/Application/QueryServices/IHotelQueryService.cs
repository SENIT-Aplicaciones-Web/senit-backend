using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;

namespace Senit.Platform.API.FrontDesk.Application.QueryServices;

/// <summary>
///     Query service contract for hotel use cases.
/// </summary>
public interface IHotelQueryService
{
    Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query, CancellationToken cancellationToken = default);

    Task<Hotel?> Handle(GetHotelByIdQuery query, CancellationToken cancellationToken = default);
}
