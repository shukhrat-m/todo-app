using API.DTOs;
using API.Interfaces;

namespace API.Services;

public class ValidationService : IValidationService
{
    public async Task<ValidationResult> Validate(TaskDTO taskDTO)
    {
        var result = new ValidationResult();

        if (taskDTO == null)
        {
            result.Errors.Add("TaskDTO is null");
        }

        if (taskDTO != null && string.IsNullOrEmpty(taskDTO.Name))
        {
            result.Errors.Add("TaskDTO does not contain name. Name field must be provided");
        }

        result.IsValid = !result.Errors.Any();

        return await Task.FromResult(result);
    }
}

public class ValidationResult
{
    public ValidationResult()
    {
        Errors = new List<string>();
    }

    public List<string> Errors { get; set; }

    public bool IsValid { get; set; }
}