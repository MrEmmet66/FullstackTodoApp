using Dapper;
using Microsoft.Data.SqlClient;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.Models;

public interface ITodoCategoryRepository
{
    List<TodoCategory> GetTodoCategories();
    TodoCategory GetTodoCategory(int id);
    void Add(TodoCategory todoCategory);
    void Update(TodoCategory todoCategory);
    void Delete(int id);
}

public class SqlTodoCategoryRepository : ITodoCategoryRepository
{
    private readonly string connectionString = null;
    public SqlTodoCategoryRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public List<TodoCategory> GetTodoCategories()
    {
        using (var db = new SqlConnection(connectionString))
        {
            return db.Query<TodoCategory>("SELECT * FROM TodoCategory").ToList();
        }
    }

    public TodoCategory GetTodoCategory(int id)
    {
        using (var db = new SqlConnection(connectionString))
        {
            return db.QueryFirstOrDefault<TodoCategory>("SELECT * FROM TodoCategory WHERE Id = @id", new { id });
        }
    }

    public void Add(TodoCategory todoCategory)
    {
        using (var db = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO TodoCategory VALUES (@Name)";
            db.Execute(query, todoCategory);
        }
    }

    public void Update(TodoCategory todoCategory)
    {
        using (var db = new SqlConnection(connectionString))
        {
            string query = "UPDATE TodoCategory SET Name = @Name WHERE Id = @Id";
            db.Execute(query, todoCategory);
        }
    }

    public void Delete(int id)
    {
        using (var db = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM TodoCategory WHERE Id = @id";
            db.Execute(query, new { id });
        }
    }
}