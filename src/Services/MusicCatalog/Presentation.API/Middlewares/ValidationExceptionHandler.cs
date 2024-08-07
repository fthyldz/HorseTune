using Application.Models.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.API.Middlewares;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        
        var errors = validationException.Errors
            .Select(errorMessage => new ValidationError(errorMessage.ErrorCode, errorMessage.ErrorMessage))
            .ToList();

        await httpContext.Response
            .WriteAsJsonAsync(new ValidationErrorResponse(errors), cancellationToken);

        return true;
    }
}