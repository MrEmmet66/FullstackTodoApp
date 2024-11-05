namespace MvcTodoApp.Models.DataTransferObjects;

public class TodoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }
    public int? CategoryId { get; set; }
}