using API.DTOs;

namespace API.Interfaces;

public interface ITasksService
{
    Task<TaskDTO> GetTaskAsync(Guid id);
    Task<IEnumerable<TaskDTO>> GetTasksAsync();
    Task<TaskDTO> UpdateAsync(TaskDTO taskDTO);
    Task<TaskDTO> CreateAsync(TaskDTO taskDTO);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<StatusDTO>> GetStatusListAsync();
}
