namespace MvcTodoApp.Models.Entities
{
	public class TodoCategory : IEntity
	{
		public TodoCategory() {}

		public TodoCategory(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public TodoCategory(string name)
		{
			Name = name;
		}
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
