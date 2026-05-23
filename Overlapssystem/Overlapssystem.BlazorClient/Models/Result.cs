namespace Overlapssystem.BlazorClient.Models;

// Represents the outcome of an operation with no return value.
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

// Represents the outcome of an operation that returns data of type T on success.
public class Result<T> : Result
{
    public T? Data { get; }

    protected Result(bool isSuccess, string? error, T? data)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, null, data);
    public new static Result<T> Failure(string error) => new(false, error, default);
}