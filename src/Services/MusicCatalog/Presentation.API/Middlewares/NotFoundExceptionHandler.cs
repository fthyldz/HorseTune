using Application.Exceptions;
using Application.Models.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.API.Middlewares;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse<object>("Not Found", notFoundException.Message), cancellationToken);

        return true;
    }
}