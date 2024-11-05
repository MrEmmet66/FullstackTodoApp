using GraphQL.Types;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.GraphQL.Types;

public class TodoCategoryType : ObjectGraphType<TodoCategory>
{
    public TodoCategoryType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
    }
}