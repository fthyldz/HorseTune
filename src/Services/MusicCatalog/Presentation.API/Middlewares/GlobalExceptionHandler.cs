using Application.Models.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.API.Middlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response
            .WriteAsJsonAsync(new ErrorResponse<object>("Internal Server Error", "An internal server error occurred. Please try again later."), cancellationToken);
        
        return true;
    }
}