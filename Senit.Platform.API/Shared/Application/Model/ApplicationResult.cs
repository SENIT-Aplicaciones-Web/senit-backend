namespace Senit.Platform.API.Shared.Application.Model;

/// <summary>
///     Represents the result of an application use case.
/// </summary>
/// <typeparam name="TValue">
///     The value returned by the use case.
/// </typeparam>
public sealed record ApplicationResult<TValue>(
    bool IsSuccess,
    TValue? Value,
    string? ErrorCode,
    int StatusCode,
    object[] ErrorArguments)
{
    public static ApplicationResult<TValue> Success(TValue value)
    {
        return new ApplicationResult<TValue>(true, value, null, StatusCodes.Status200OK, Array.Empty<object>());
    }

    public static ApplicationResult<TValue> Created(TValue value)
    {
        return new ApplicationResult<TValue>(true, value, null, StatusCodes.Status201Created, Array.Empty<object>());
    }

    public static ApplicationResult<TValue> Failure(string errorCode, int statusCode, params object[] errorArguments)
    {
        return new ApplicationResult<TValue>(false, default, errorCode, statusCode, errorArguments);
    }
}
