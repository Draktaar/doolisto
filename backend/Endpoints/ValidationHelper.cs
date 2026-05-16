using System.ComponentModel.DataAnnotations;

namespace backend.Endpoints;

public static class ValidationHelper
{
    public static IResult? Validate<T>(T request) where T : notnull
    {
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, context, results, validateAllProperties: true);

        if (isValid)
            return null;

        var errors = results
            .SelectMany(r => r.MemberNames.Select(m => new { field = m, error = r.ErrorMessage }))
            .ToList();

        return Results.BadRequest(new { errors });
    }
}