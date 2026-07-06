using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Senit.Platform.API.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace Senit.Platform.API.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

/// <summary>
///     Replaces controller route tokens with kebab case names.
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(action => action.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
    }

    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }
}
