using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Payment.Application.CommandServices;
using Senit.Platform.API.Payment.Application.QueryServices;
using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Domain.Model.Queries;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;
using Senit.Platform.API.Payment.Interfaces.Rest.Transform;
using Senit.Platform.API.Payment.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Payment.Interfaces.Rest;

/// <summary>
///     REST controller for invoices.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Invoices endpoints")]
public class InvoicesController(
    IInvoiceQueryService queryService,
    IInvoiceCommandService commandService,
    IStringLocalizer<PaymentMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<PaymentMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Invoices are issued by payment workflows, edition and deletion are not exposed by the current UI.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all invoices",
        Description = "Get invoices. When hotelId is provided, only invoices linked to payments of the requested hotel are returned.",
        OperationId = "GetAllInvoices")]
    [SwaggerResponse(StatusCodes.Status200OK, "The invoices were found", typeof(IEnumerable<InvoiceResource>))]
    public async Task<IActionResult> GetAllInvoices(
        [SwaggerParameter("Hotel identifier used to return only invoices owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllInvoicesQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a invoice",
        Description = "Create a invoice",
        OperationId = "CreateInvoice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The invoice was created", typeof(InvoiceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The invoice was not created")]
    public async Task<IActionResult> CreateInvoice(
        [FromBody] CreateInvoiceResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return InvoiceActionResultAssembler.ToActionResultFromCreateInvoiceResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                InvoiceResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }



}
