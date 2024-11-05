using Dapper;
using System.Linq;
using Microsoft.Data.SqlClient;
using MvcTodoApp.Models.Entities;
using System.Data;
using System.Collections.Immutable;
using System.Diagnostics.SymbolStore;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using MvcTodoApp.Models.DataTransferObjects;

namespace MvcTodoApp.Models
{
    public interface ITodoItemRepository
    {
        List<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(int id);
        TodoItem Add(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void Delete(int id);
    }

    public class SqlTodoItemRepository : ITodoItemRepository
    {
        string connectionString = null;
        private readonly IMapper mapper;

        public SqlTodoItemRepository(string connectionString, IMapper mapper)
        {
            this.connectionString = connectionString;
            this.mapper = mapper;
        }

        public TodoItem Add(TodoItem todoItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                TodoItemDto todoItemDto = mapper.Map<TodoItemDto>(todoItem);
                string query = "INSERT INTO TodoItem (Title, IsDone, CreatedAt, CompletedAt, CategoryId) OUTPUT INSERTED.* VALUES (@Title, @IsDone, @CreatedAt, @CompletedAt, @CategoryId)";
                TodoItemDto insertedTodoItemDto = db.QueryFirst<TodoItemDto>(query, todoItemDto);
                return GetTodoItem(insertedTodoItemDto.Id);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TodoItem WHERE Id = @id";
                db.Execute(query, new { id });
            }
        }

        public TodoItem GetTodoItem(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TodoItem" +
                               " LEFT OUTER JOIN TodoCategory" +
                               " ON TodoItem.CategoryId = TodoCategory.Id" +
                               " WHERE TodoItem.Id = @id";
                return db.Query<TodoItem, TodoCategory, TodoItem>(query, (item, category) =>
                {
                    if(category.Id != 0) { item.Category = category; }
                    return item;
                }, new { id }, splitOn: "CategoryId").FirstOrDefault();
            }
        }

        public List<TodoItem> GetTodoItems()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TodoItem i LEFT OUTER JOIN TodoCategory c ON i.CategoryId = c.Id";
                return db.Query<TodoItem, TodoCategory, TodoItem>(query, (item, category) =>
                {
                    if(category.Id != 0)
                        item.Category = category;
                    return item;
                }, splitOn: "CategoryId").ToList();
            }
        }

        public void Update(TodoItem todoItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                TodoItemDto todoItemDto = mapper.Map<TodoItemDto>(todoItem);
                string query =
                    "UPDATE TodoItem SET Title = @Title, IsDone = @IsDone, CreatedAt = @CreatedAt, CompletedAt = @CompletedAt, CategoryId = @CategoryId WHERE Id = @Id";
                db.Execute(query, todoItemDto);
            }
        }
    }
}