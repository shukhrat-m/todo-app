using API.Interfaces;

namespace API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ITasksRepository _tasksRepository;

    public UnitOfWork(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public ITasksRepository TasksRepository => _tasksRepository;
}
