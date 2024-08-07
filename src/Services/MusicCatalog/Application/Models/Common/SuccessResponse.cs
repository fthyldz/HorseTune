namespace Application.Models.Common;

public class SuccessResponse<TData>(TData? data, string message) : Response(true, message), IDataResponse<TData>
{
    public TData? Data { get; init; } = data;
}