using GraphQL.Types;
using MvcTodoApp.Models.DataTransferObjects;

namespace MvcTodoApp.GraphQL.Types.InputTypes;

public class TodoItemInputType : InputObjectGraphType<TodoItemDto>
{
    
    public TodoItemInputType()
    {
        Field(x => x.Title);
        Field(x => x.IsDone).DefaultValue(false);
        Field(x => x.CreatedAt).DefaultValue(DateTime.Now);
        Field(x => x.CompletedAt, nullable: true);
        Field(x => x.CategoryId, nullable: true);
    }
    
    public TodoItemInputType(TodoItemDto dto)
    {
        Field(x => x.Title).DefaultValue(dto.Title);
        Field(x => x.IsDone).DefaultValue(dto.IsDone);
        Field(x => x.CreatedAt).DefaultValue(dto.CreatedAt);
        Field(x => x.CompletedAt, nullable: true).DefaultValue(dto.CompletedAt);
        Field(x => x.CategoryId, nullable: true).DefaultValue(dto.CategoryId);
    }
}