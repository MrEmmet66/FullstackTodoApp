using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTodoApp.Models.Entities
{
	public class TodoItem : IEntity
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public bool IsDone { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime? CompletedAt { get; set; }
		public TodoCategory? Category { get; set; }
	}
}
