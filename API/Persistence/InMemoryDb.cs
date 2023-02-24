using API.Persistence.Entities;

namespace API.Persistence;

public sealed class InMemoryDb<T>
{
    public List<T> Entitites { get; set; }

    private static readonly object _locker = new object();
    private static InMemoryDb<T> _instance;
    
    private InMemoryDb() 
    {
        Entitites = new List<T>();
    }

    public static InMemoryDb<T> GetInstance()
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new InMemoryDb<T>();
                }
            }
        }
        return _instance;
    }
}