using Application.Models.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.API.Middlewares;

public class BadHttpRequestExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badHttpRequestException)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ValidationErrorResponse([
            new ValidationError("BadHttpRequest", badHttpRequestException.Message)
        ]);

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}