using AutoMapper;
using GraphQL;
using GraphQL.Types;
using MvcTodoApp.GraphQL.Types;
using MvcTodoApp.GraphQL.Types.InputTypes;
using MvcTodoApp.Models;
using MvcTodoApp.Models.DataTransferObjects;
using MvcTodoApp.Models.Entities;
using MvcTodoApp.Utils;

namespace MvcTodoApp.GraphQL.Mutations;

public class TodoItemMutation : ObjectGraphType
{
    public TodoItemMutation(IRepositoryFactoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        Field<AutoRegisteringObjectGraphType<TodoItem>>("addTodoItem",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TodoItemInputType>> { Name = "todoItem" }),
            resolve: context =>
            {
                string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
                if (storageType is null)
                    storageType = StorageTypes.MsSqlDb;
                ITodoItemRepository todoItemRepository = repositoryProvider.CreateRepositoryFactory(storageType).CreateTodoItemRepository();
                var todoItemDto = context.GetArgument<TodoItemDto>("todoItem");
                TodoItem todoItem = mapper.Map<TodoItem>(todoItemDto);
                return todoItemRepository.Add(todoItem);
            });
        
        Field<AutoRegisteringObjectGraphType<TodoItem>>("updateTodoItemStatus",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<BooleanGraphType>> { Name = "isDone" }),
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");
                bool isDone = context.GetArgument<bool>("isDone");
                string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
                if (storageType is null)
                    storageType = StorageTypes.MsSqlDb;
                ITodoItemRepository todoItemRepository = repositoryProvider.CreateRepositoryFactory(storageType).CreateTodoItemRepository();
                TodoItem todoItem = todoItemRepository.GetTodoItem(id);
                todoItem.IsDone = isDone;
                todoItemRepository.Update(todoItem);
                return todoItem;
            });
        Field<AutoRegisteringObjectGraphType<TodoItem>>("deleteTodoItem",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id" }),
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");
                string storageType = httpContextAccessor.HttpContext.Request.Headers[SessionConstants.StorageType];
                if (storageType is null)
                    storageType = StorageTypes.MsSqlDb;
                ITodoItemRepository todoItemRepository = repositoryProvider.CreateRepositoryFactory(storageType).CreateTodoItemRepository();
                TodoItem todoItem = todoItemRepository.GetTodoItem(id);
                todoItemRepository.Delete(id);
                return todoItem;
            });
    }
}