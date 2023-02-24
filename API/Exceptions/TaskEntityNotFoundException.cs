namespace API.Exceptions;

public class TaskEntityNotFoundException : Exception
{
    public TaskEntityNotFoundException(string? message) : base(message)
    {
    }
}
