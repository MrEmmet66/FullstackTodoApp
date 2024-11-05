using AutoMapper;
using MvcTodoApp.Models.Repositories;

namespace MvcTodoApp.Models.Factories;

public class XmlRepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider serviceProvider;
    public XmlRepositoryFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ITodoItemRepository CreateTodoItemRepository()
    {
        return serviceProvider.GetRequiredService<XmlTodoItemRepository>();
    }

    public ITodoCategoryRepository CreateTodoCategoryRepository()
    {
        return serviceProvider.GetRequiredService<XmlTodoCategoryRepository>();
    }
}