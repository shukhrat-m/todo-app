using API.Persistence.Entities;

namespace API.Interfaces;

public interface ITasksRepository : IRepository<TaskEntity>
{
    Task<TaskEntity> UpdateAsync(TaskEntity entity);

    Task<bool> IsExistByNameAsync(string taskName);
}
