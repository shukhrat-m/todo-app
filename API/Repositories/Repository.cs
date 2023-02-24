using API.Interfaces;
using API.Persistence;
using API.Persistence.Entities;

namespace API.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly InMemoryDb<T> _db;

    public Repository(InMemoryDb<T> db)
    {
        _db = db;
    }

    public async Task<T> CreateAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        _db.Entitites.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = _db.Entitites.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _db.Entitites.Remove(item);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return  await Task.FromResult(_db.Entitites);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(_db.Entitites.FirstOrDefault(x => x.Id == id));
    }

    //Because we are not using EF for instance, there will not be any way to implement generic method to update entity
    //Gonna be specific
}
