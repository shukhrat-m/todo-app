namespace API.Interfaces;

public interface IUnitOfWork
{
    ITasksRepository TasksRepository { get; }
}
