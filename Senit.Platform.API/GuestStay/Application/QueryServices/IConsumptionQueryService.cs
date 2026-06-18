using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;

namespace Senit.Platform.API.GuestStay.Application.QueryServices;

/// <summary>
///     Query service contract for consumption use cases.
/// </summary>
public interface IConsumptionQueryService
{
    Task<IEnumerable<Consumption>> Handle(GetAllConsumptionsQuery query, CancellationToken cancellationToken = default);

    Task<Consumption?> Handle(GetConsumptionByIdQuery query, CancellationToken cancellationToken = default);
}
