using API.Interfaces;
using API.Persistence;
using API.Persistence.Entities;
using API.Repositories;
using API.Services;
using System.Reflection;

namespace API.Extensions;

public static class TodoAppExtension
{
    public static IServiceCollection AddTodoAppServices (this IServiceCollection services)
    {
        //Repositories and UnitOfWork layers
        services.AddScoped<ITasksRepository, TasksRepository>(sp =>
        {
            var db = InMemoryDb<TaskEntity>.GetInstance();
            return new TasksRepository(db);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Adding validation service
        services.AddScoped<IValidationService, ValidationService>();

        //Adding automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Services
        services.AddScoped<ITasksService, TasksService>();

        return services;
    }
}
