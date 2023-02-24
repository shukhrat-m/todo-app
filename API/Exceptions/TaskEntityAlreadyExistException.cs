namespace API.Exceptions;

public class TaskEntityAlreadyExistException : Exception
{
    public TaskEntityAlreadyExistException(string? message) : base(message)
    {
    }
}
