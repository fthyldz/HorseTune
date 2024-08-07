namespace Application.Models.Common;

public interface IResponse
{
    public bool Success { get; init; }
    public string Message { get; init; }
}