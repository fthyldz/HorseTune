namespace Application.Models.Common;

public class Response(bool success, string message) : IResponse
{
    public bool Success { get; init; } = success;
    public string Message { get; init; } = message;
}