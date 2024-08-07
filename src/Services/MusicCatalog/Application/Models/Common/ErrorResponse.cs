namespace Application.Models.Common;

public class ErrorResponse<T>(string error, string message) : Response(false, message), IErrorResponse, IDataResponse<T>
{
    public string Error { get; init; } = error;

    public T? Data { get; init; } = default;
}