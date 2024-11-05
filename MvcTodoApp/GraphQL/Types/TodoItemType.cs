using GraphQL.Types;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.GraphQL.Types;

public class TodoItemType : ObjectGraphType<TodoItem>    
{
    public TodoItemType()
    {
        Field(x => x.Id);
        Field(x => x.Title);
        Field(x => x.IsDone);
        Field(x => x.CreatedAt);
        Field(x => x.CompletedAt, nullable: true);
        Field(x => x.Category, nullable:true);
    }
}