namespace Application.Commons.Domain;

public class Response<T>
{
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    public static Response<T> Success(T result) =>
        new Response<T> { Result = result };

    public static Response<T> Error(string errorMessage) =>
        new Response<T> { ErrorMessage = errorMessage };
}