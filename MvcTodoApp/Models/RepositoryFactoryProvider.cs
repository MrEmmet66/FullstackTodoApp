using MvcTodoApp.Models.Factories;
using MvcTodoApp.Utils;

namespace MvcTodoApp.Models;

public interface IRepositoryFactoryProvider
{
    IRepositoryFactory CreateRepositoryFactory(string storageType = StorageTypes.MsSqlDb);
}

public class RepositoryFactoryProvider : IRepositoryFactoryProvider
{
    private readonly IHttpContextAccessor httpContext;
    private readonly IServiceProvider serviceProvider;
    public RepositoryFactoryProvider(IHttpContextAccessor httpContext, IServiceProvider serviceProvider)
    {
        this.httpContext = httpContext;
        this.serviceProvider = serviceProvider;
        this.httpContext = httpContext;
    }
    
    public IRepositoryFactory CreateRepositoryFactory(string storageType = StorageTypes.MsSqlDb)
    {
        switch (storageType)
        {
            case StorageTypes.MsSqlDb:
                return new SqlRepositoryFactory(serviceProvider);
            case StorageTypes.XmlStorage:
                return new XmlRepositoryFactory(serviceProvider);
            default:
                throw new ArgumentException("Invalid storage type.");
        }
    }

}