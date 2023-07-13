namespace Domain.Base;

public class BaseResponse<T>
{
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    public static BaseResponse<T> Success(T result) =>
        new BaseResponse<T> { Result = result };

    public static BaseResponse<T> Error(string errorMessage) =>
        new BaseResponse<T> { ErrorMessage = errorMessage };
}