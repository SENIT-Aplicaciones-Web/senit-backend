using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Resources.Errors;
using Senit.Platform.API.Resources.Shared;

namespace Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Factory for creating API Problem Details responses with localized messages.
/// </summary>
public class ProblemDetailsFactory
{
    private readonly Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory _aspNetCoreProblemDetailsFactory;
    private readonly IStringLocalizer<CommonMessages> _commonLocalizer;
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProblemDetailsFactory" /> class.
    /// </summary>
    public ProblemDetailsFactory(
        IStringLocalizer<ErrorMessages> errorLocalizer,
        IStringLocalizer<CommonMessages> commonLocalizer,
        Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
    {
        _errorLocalizer = errorLocalizer;
        _commonLocalizer = commonLocalizer;
        _aspNetCoreProblemDetailsFactory = aspNetCoreProblemDetailsFactory;
    }

    /// <summary>
    ///     Creates a problem details response using an error enum as title key.
    /// </summary>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum,
        string detailMessage)
    {
        var title = errorEnum != null
            ? Localize(_errorLocalizer, $"{errorEnum}")
            : Localize(_errorLocalizer, "GenericError");

        return CreateProblemDetailsResult(controller, statusCode, title, detailMessage);
    }

    /// <summary>
    ///     Creates a problem details response using localized title and detail keys.
    /// </summary>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey,
        string detailKey,
        params object[] detailArgs)
    {
        var title = Localize(_commonLocalizer, titleKey);
        var detail = Localize(_errorLocalizer, detailKey, detailArgs);

        return CreateProblemDetailsResult(controller, statusCode, title, detail);
    }

    /// <summary>
    ///     Creates a problem details response using an application error code and the context localizer.
    /// </summary>
    public IActionResult CreateErrorProblemDetails(
        ControllerBase controller,
        int statusCode,
        string errorCode,
        IStringLocalizer contextLocalizer,
        params object[] errorArguments)
    {
        var title = Localize(contextLocalizer, errorCode, errorArguments);
        var detail = title;

        return CreateProblemDetailsResult(controller, statusCode, title, detail);
    }

    /// <summary>
    ///     Creates a problem details response using a shared application error code.
    /// </summary>
    public IActionResult CreateErrorProblemDetails(
        ControllerBase controller,
        int statusCode,
        string errorCode,
        params object[] errorArguments)
    {
        var title = Localize(_errorLocalizer, errorCode, errorArguments);
        var detail = title;

        return CreateProblemDetailsResult(controller, statusCode, title, detail);
    }

    private string Localize(IStringLocalizer localizer, string key, params object[] arguments)
    {
        var localized = arguments.Length > 0 ? localizer[key, arguments] : localizer[key];

        if (!localized.ResourceNotFound)
            return localized.Value;

        var sharedError = arguments.Length > 0 ? _errorLocalizer[key, arguments] : _errorLocalizer[key];

        if (!sharedError.ResourceNotFound)
            return sharedError.Value;

        var common = arguments.Length > 0 ? _commonLocalizer[key, arguments] : _commonLocalizer[key];

        return common.ResourceNotFound ? key : common.Value;
    }

    private IActionResult CreateProblemDetailsResult(
        ControllerBase controller,
        int statusCode,
        string title,
        string detail)
    {
        var problemDetails = _aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            title,
            detail: detail,
            instance: controller.HttpContext.Request.Path);

        problemDetails.Title = title;
        problemDetails.Detail = detail;
        problemDetails.Instance = controller.HttpContext.Request.Path;

        return controller.StatusCode(statusCode, problemDetails);
    }
}
