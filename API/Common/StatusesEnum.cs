using System.ComponentModel.DataAnnotations;

namespace API.Common;

public enum StatusesEnum
{
    [Display(Name = "Not started")]
    NotStarted,
    [Display(Name = "In progress")]
    InProgress,
    [Display(Name = "Completed")]
    Completed
}
