using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.FrontDesk.Application.CommandServices;

/// <summary>
///     Command service contract for hotel use cases.
/// </summary>
public interface IHotelCommandService
{
    Task<ApplicationResult<Hotel>> Handle(CreateHotelCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Hotel>> Handle(UpdateHotelCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteHotelCommand command, CancellationToken cancellationToken = default);
}
