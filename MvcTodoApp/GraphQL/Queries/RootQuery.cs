using GraphQL.Types;

namespace MvcTodoApp.GraphQL;

public class RootQuery : ObjectGraphType
{
    public RootQuery()
    {
        Field<TodoItemQuery>("todoItemQuery").Resolve(context => new {});
        Field<TodoCategoryQuery>("todoCategoryQuery").Resolve(context => new {});
    }
}