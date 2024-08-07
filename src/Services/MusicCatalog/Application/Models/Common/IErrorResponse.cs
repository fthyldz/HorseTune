namespace Application.Models.Common;

public interface IErrorResponse : IResponse
{
    public string Error { get; init; }
}