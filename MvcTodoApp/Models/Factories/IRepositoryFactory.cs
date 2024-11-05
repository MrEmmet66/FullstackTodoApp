namespace MvcTodoApp.Models.Factories;

public interface IRepositoryFactory
{
    ITodoItemRepository CreateTodoItemRepository();
    ITodoCategoryRepository CreateTodoCategoryRepository();
}