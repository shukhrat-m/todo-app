using API.Exceptions;
using API.Interfaces;
using API.Persistence;
using API.Persistence.Entities;

namespace API.Repositories;

public class TasksRepository : Repository<TaskEntity>, ITasksRepository
{
    private readonly InMemoryDb<TaskEntity> _db;

    public TasksRepository(InMemoryDb<TaskEntity> db) : base(db)
    {
        _db = db;
    }

    public Task<bool> IsExistByNameAsync(string taskName)
    {
        return Task.FromResult(_db.Entitites.Any(x => x.Name.Equals(taskName, StringComparison.InvariantCultureIgnoreCase)));

    }

    public async Task<TaskEntity> UpdateAsync(TaskEntity entity)
    {
        var item = _db.Entitites.FirstOrDefault(x => x.Id == entity.Id);
        
        if (item == null)
        {
            throw new TaskEntityNotFoundException($"Task with provided id '{entity.Id}' does not exist.");
        }

        item.Name = entity.Name;
        item.Priority = entity.Priority;
        item.Status = entity.Status;

        return await Task.FromResult(item);
    }
}
