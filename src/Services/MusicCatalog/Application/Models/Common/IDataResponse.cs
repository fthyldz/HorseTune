namespace Application.Models.Common;

public interface IDataResponse<TData> : IResponse
{
    public TData? Data { get; init; }
}