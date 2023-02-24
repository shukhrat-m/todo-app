using API.DTOs;
using API.Services;

namespace API.Interfaces;

public interface IValidationService
{
    Task<ValidationResult> Validate(TaskDTO taskDTO);
}
