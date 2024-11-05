using System.Data.Common;

namespace MvcTodoApp.Utils
{
	public class DbConnectionStringValidator : IStringValidator
	{
		public bool Validate(string input)
		{
			DbConnectionStringBuilder dsb = new DbConnectionStringBuilder();
			dsb.ConnectionString = input;
			return dsb.ContainsKey("Data Source");
		}
	}
}
