namespace Senit.Platform.API.Shared.Application.Model;

/// <summary>
///     Generic result wrapper used by command services.
/// </summary>
/// <typeparam name="T">The success value type.</typeparam>
public class Result<T>
{
    private Result(bool isSuccess, T? value, Enum? error, string message)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Message = message;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public Enum? Error { get; }
    public string Message { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, string.Empty);
    }

    public static Result<T> Failure(Enum error, string message)
    {
        return new Result<T>(false, default, error, message);
    }
}

/// <summary>
///     Non-generic result wrapper used by command services that do not return a value.
/// </summary>
public class Result
{
    private Result(bool isSuccess, Enum? error, string message)
    {
        IsSuccess = isSuccess;
        Error = error;
        Message = message;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Enum? Error { get; }
    public string Message { get; }

    public static Result Success()
    {
        return new Result(true, null, string.Empty);
    }

    public static Result Failure(Enum error, string message)
    {
        return new Result(false, error, message);
    }
}
