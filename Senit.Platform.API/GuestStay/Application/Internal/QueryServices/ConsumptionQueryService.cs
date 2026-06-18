using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;
using Senit.Platform.API.GuestStay.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for consumption use cases.
/// </summary>
public class ConsumptionQueryService(IConsumptionRepository repository) : IConsumptionQueryService
{
    public async Task<IEnumerable<Consumption>> Handle(GetAllConsumptionsQuery query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(query.HotelId))
            return await repository.ListByHotelIdAsync(query.HotelId, cancellationToken);

        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Consumption?> Handle(GetConsumptionByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
