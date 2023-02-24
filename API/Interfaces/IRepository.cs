namespace API.Interfaces;

public interface IRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task DeleteAsync(Guid id);
}
