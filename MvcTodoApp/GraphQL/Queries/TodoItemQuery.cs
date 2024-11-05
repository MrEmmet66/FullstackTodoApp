using GraphQL;
using GraphQL.Types;
using MvcTodoApp.GraphQL.Types;
using MvcTodoApp.Models;
using MvcTodoApp.Utils;

namespace MvcTodoApp.GraphQL;

public class TodoItemQuery : ObjectGraphType
{
    public TodoItemQuery(IRepositoryFactoryProvider repositoryProvider, IHttpContextAccessor httpContextAccessor)
    {
        Field<ListGraphType<TodoItemType>>("todoItems", resolve: context =>
        {
            string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
            if (storageType is null)
                storageType = StorageTypes.MsSqlDb;
            return repositoryProvider.CreateRepositoryFactory(storageType).CreateTodoItemRepository().GetTodoItems();
        });
        Field<TodoItemType>("todoItem",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve:
            context =>
            {
                int id = context.GetArgument<int>("id");
                string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
                if (storageType is null)
                    storageType = StorageTypes.MsSqlDb;
                return repositoryProvider.CreateRepositoryFactory(storageType).CreateTodoItemRepository().GetTodoItem(id);
            });
    }
}