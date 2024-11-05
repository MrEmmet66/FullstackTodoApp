using GraphQL.Types;
using MvcTodoApp.GraphQL.Mutations;
using MvcTodoApp.GraphQL.Types;
using MvcTodoApp.GraphQL.Types.InputTypes;
using MvcTodoApp.Models.DataTransferObjects;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.GraphQL;

public class TodoItemSchema : Schema
{
    public TodoItemSchema(IServiceProvider provider) : base(provider)
    {
        this.RegisterTypeMapping(typeof(TodoItem), typeof(TodoItemType));
        this.RegisterTypeMapping(typeof(TodoCategory), typeof(TodoCategoryType));
        this.RegisterTypeMapping(typeof(TodoItemDto), typeof(TodoItemInputType));
        Query = provider.GetRequiredService<RootQuery>();
        Mutation = provider.GetRequiredService<TodoItemMutation>();
    }
}