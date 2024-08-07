namespace Application.Models.Common;

public class ValidationErrorResponse(List<ValidationError> details) : ErrorResponse<ValidationError>("Validation Error", "One or more validation errors occurred.")
{
    public List<ValidationError> Details { get; set; } = details;
}

public class ValidationError(string field, string message)
{
    public string Field { get; init; } = field;
    public string Message { get; init; } = message;
}