using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class TaskDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? PriorityValue { get; set; }
    public int? Status { get; set; }
    public string StatusText { get; set; } = string.Empty;
}
