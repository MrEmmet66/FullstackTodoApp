using GraphQL;
using GraphQL.Types;
using MvcTodoApp.GraphQL.Types;
using MvcTodoApp.Models;
using MvcTodoApp.Utils;

namespace MvcTodoApp.GraphQL;

public class TodoCategoryQuery : ObjectGraphType
{
    public TodoCategoryQuery(IRepositoryFactoryProvider repositoryFactoryProvider, IHttpContextAccessor httpContextAccessor)
    {
        Field<ListGraphType<TodoCategoryType>>("todoCategories").Resolve(context =>
        {
            string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
            if (storageType is null)
                storageType = StorageTypes.MsSqlDb;
            return repositoryFactoryProvider.CreateRepositoryFactory(storageType).CreateTodoCategoryRepository()
                .GetTodoCategories();
        });
        
        Field<TodoCategoryType>("todoCategory",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve:
            context =>
            {
                int id = context.GetArgument<int>("id");
                string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
                if (storageType is null)
                    storageType = StorageTypes.MsSqlDb;
                return repositoryFactoryProvider.CreateRepositoryFactory(storageType).CreateTodoCategoryRepository()
                    .GetTodoCategory(id);
            });
    }
}