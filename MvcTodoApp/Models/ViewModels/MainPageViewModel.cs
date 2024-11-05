using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.Models.ViewModels;

public class MainPageViewModel
{
    public MainPageViewModel() {}
    public MainPageViewModel(IEnumerable<TodoItem> todoItems, IEnumerable<TodoCategory> todoCategories)
    {
        TodoItems = todoItems;
        TodoCategories = todoCategories;
    }
    public IEnumerable<TodoItem> TodoItems { get; set; }
    public IEnumerable<TodoCategory> TodoCategories { get; set; }
    public TodoItem NewTodoItem { get; set; }
    public string StorageType { get; set; }
}