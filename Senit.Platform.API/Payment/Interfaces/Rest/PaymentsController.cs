using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Payment.Application.CommandServices;
using Senit.Platform.API.Payment.Application.QueryServices;
using Senit.Platform.API.Payment.Domain.Model.Queries;
using Senit.Platform.API.Payment.Interfaces.Rest.Resources;
using Senit.Platform.API.Payment.Interfaces.Rest.Transform;
using Senit.Platform.API.Payment.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Payment.Interfaces.Rest;

/// <summary>
///     REST controller for payments.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payments endpoints")]
public class PaymentsController(
    IPaymentQueryService queryService,
    IPaymentCommandService commandService,
    IStringLocalizer<PaymentMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<PaymentMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Payments are confirmed once through checkout. Payment edition is not exposed by the current UI.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all payments",
        Description = "Get all payments",
        OperationId = "GetAllPayments")]
    [SwaggerResponse(StatusCodes.Status200OK, "The payments were found", typeof(IEnumerable<PaymentResource>))]
    public async Task<IActionResult> GetAllPayments(CancellationToken cancellationToken)
    {
        var query = new GetAllPaymentsQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a payment",
        Description = "Create a payment",
        OperationId = "CreatePayment")]
    [SwaggerResponse(StatusCodes.Status201Created, "The payment was created", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The payment was not created")]
    public async Task<IActionResult> CreatePayment(
        [FromBody] CreatePaymentResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreatePaymentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return PaymentActionResultAssembler.ToActionResultFromCreatePaymentResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                PaymentResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }
}
