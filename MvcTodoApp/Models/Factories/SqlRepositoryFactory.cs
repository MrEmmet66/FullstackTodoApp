using AutoMapper;

namespace MvcTodoApp.Models.Factories;

public class SqlRepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider serviceProvider;
    
    public SqlRepositoryFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    
    public ITodoItemRepository CreateTodoItemRepository()
    {
        return serviceProvider.GetRequiredService<SqlTodoItemRepository>();
    }
    
    public ITodoCategoryRepository CreateTodoCategoryRepository()
    {
        return serviceProvider.GetRequiredService<SqlTodoCategoryRepository>();
    }
    
    
    
}