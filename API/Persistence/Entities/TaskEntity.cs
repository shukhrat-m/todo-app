using System.ComponentModel.DataAnnotations;

namespace API.Persistence.Entities;

public class TaskEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public int? Priority { get; set; }
    public int? Status { get; set; }
}
