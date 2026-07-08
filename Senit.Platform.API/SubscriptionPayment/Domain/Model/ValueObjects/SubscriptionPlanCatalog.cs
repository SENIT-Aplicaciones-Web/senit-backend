namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.ValueObjects;

/// <summary>
///     Provides supported subscription plan prices for Senit.
/// </summary>
public static class SubscriptionPlanCatalog
{
    public static bool IsSupported(string plan)
    {
        return Normalize(plan) is "Basic" or "Pro";
    }

    public static string Normalize(string plan)
    {
        var value = string.IsNullOrWhiteSpace(plan) ? "Basic" : plan.Trim();
        return value.Equals("Pro", StringComparison.OrdinalIgnoreCase) ? "Pro" : "Basic";
    }

    public static decimal GetMonthlyAmount(string plan)
    {
        return Normalize(plan) == "Pro" ? 49.99m : 29.99m;
    }
}
