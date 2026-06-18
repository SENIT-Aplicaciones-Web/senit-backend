using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Payment.Application.CommandServices;

/// <summary>
///     Command service contract for invoice use cases.
/// </summary>
public interface IInvoiceCommandService
{
    Task<ApplicationResult<Invoice>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Invoice>> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteInvoiceCommand command, CancellationToken cancellationToken = default);
}
