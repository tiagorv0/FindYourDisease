using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Patient.Abstractions;

public class Result<T> where T : class
{
    private Result(Error error)
    {
        if(error == Error.None)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = false;
        Error = error;
    }

    private Result(T data, Error error)
    {
        if (data == null && error == Error.None)
            throw new ArgumentException("Invalid data", nameof(data));

        IsSuccess = true;
        Data = data;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Data { get; }
    public Error Error { get; }


    public static Result<T> Success(T data) => new(data, Error.None);
    public static Result<T?> Success() => new(Error.None);
    public static Result<T?> Failure(Error error) => new(error);
}
