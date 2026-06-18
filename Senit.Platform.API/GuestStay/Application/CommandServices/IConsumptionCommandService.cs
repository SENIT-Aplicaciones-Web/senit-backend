using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.GuestStay.Application.CommandServices;

/// <summary>
///     Command service contract for consumption use cases.
/// </summary>
public interface IConsumptionCommandService
{
    Task<ApplicationResult<Consumption>> Handle(CreateConsumptionCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Consumption>> Handle(UpdateConsumptionCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteConsumptionCommand command, CancellationToken cancellationToken = default);
}
