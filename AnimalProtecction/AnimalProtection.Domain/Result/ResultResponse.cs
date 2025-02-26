namespace AnimalProtection.Domain.Result;

public class ResultResponse<T>
{
    public T Value { get; init; }
    public bool IsSuccess { get; init; }
    public string Error { get; init; }
    public int Code { get; init; }

    private ResultResponse(T value, bool isSuccess, string error, int code)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
        Code = code;
    }

    // Success factory method
    public static ResultResponse<T> Success(T value, int code = 200) =>
        new(value, true, string.Empty, code);

    // Failure factory methods
    public static ResultResponse<T> Failure(string error, int code = 500) =>
        new(default, false, error, code);

    public static ResultResponse<T> Failure(T value, string error, int code = 500) =>
        new(value, false, error, code);



}