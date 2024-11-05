using System.Xml.Linq;
using AutoMapper;
using MvcTodoApp.Models.DataTransferObjects;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.Models.Repositories;

public class XmlTodoItemRepository : ITodoItemRepository
{
    private readonly string xmlPath;
    private readonly IMapper mapper;
    private readonly string xmlCategoryPath;
    
    public XmlTodoItemRepository(string xmlPath, IMapper mapper, string xmlCategoryPath)
    {
        this.mapper = mapper;
        this.xmlPath = xmlPath;
        this.xmlCategoryPath = xmlCategoryPath;
        
        FileInfo fileInfo = new FileInfo(xmlPath);
        if (!fileInfo.Exists)
        {
            XDocument xdoc = new XDocument(new XElement("TodoItems"));
            xdoc.Save(xmlPath);
        }
        
    }

    public List<TodoItem> GetTodoItems()
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoCategories = XDocument.Load(xmlCategoryPath).Root.Elements("TodoCategory")
            .Select(x => new TodoCategory(int.Parse(x.Element("Id").Value), x.Element("Name").Value));
        var todoItems = xdoc.Root.Elements("TodoItem")
            .Select(x => new TodoItem
            {
                Id = int.Parse(x.Element("Id").Value),
                Title = x.Element("Title").Value,
                IsDone = bool.Parse(x.Element("IsDone").Value),
                CreatedAt = DateTime.Parse(x.Element("CreatedAt").Value),
                CompletedAt = DateTime.TryParse(x.Element("CompletedAt")?.Value, out DateTime tempDate)
                    ? tempDate
                    : (DateTime?)null,
                Category = x.Element("CategoryId") != null && int.TryParse(x.Element("CategoryId").Value, out int categoryId) 
                    ? todoCategories.FirstOrDefault(c => c.Id == categoryId) 
                    : null
            });
        return todoItems.ToList();
    }

    public TodoItem GetTodoItem(int id)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoCategories = XDocument.Load(xmlCategoryPath).Root.Elements("TodoCategory")
            .Select(x => new TodoCategory(int.Parse(x.Element("Id").Value), x.Element("Name").Value));
        var todoItem = xdoc.Root.Elements("TodoItem")
            .Where(x => int.Parse(x.Element("Id").Value) == id)
            .Select(x => new TodoItem
            {
                Id = int.Parse(x.Element("Id").Value),
                Title = x.Element("Title").Value,
                IsDone = bool.Parse(x.Element("IsDone").Value),
                CreatedAt = DateTime.Parse(x.Element("CreatedAt").Value),
                CompletedAt = DateTime.TryParse(x.Element("CompletedAt")?.Value, out DateTime tempDate)
                    ? tempDate
                    : null,
                Category = x.Element("CategoryId") != null && int.TryParse(x.Element("CategoryId").Value, out int categoryId) 
                    ? todoCategories.FirstOrDefault(c => c.Id == categoryId) 
                    : null
            });
        return todoItem.FirstOrDefault();
    }

    public TodoItem Add(TodoItem todoItem)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoItems = xdoc.Root.Elements("TodoItem");
        int maxId = todoItems.Any() ? todoItems.Max(x => int.Parse(x.Element("Id").Value)) : 0;
        todoItem.Id = maxId + 1;
    
        XElement todoItemElement = new XElement("TodoItem",
            new XElement("Id", todoItem.Id),
            new XElement("Title", todoItem.Title),
            new XElement("IsDone", todoItem.IsDone),
            new XElement("CreatedAt", todoItem.CreatedAt),
            new XElement("CompletedAt", todoItem.CompletedAt),
            new XElement("CategoryId", todoItem.Category != null ? todoItem.Category.Id : null)
        );
        xdoc.Root.Add(todoItemElement);
        xdoc.Save(xmlPath);

        // Reload the XML document and return the newly added TodoItem
        xdoc = XDocument.Load(xmlPath);
        var todoCategories = XDocument.Load(xmlCategoryPath).Root.Elements("TodoCategory")
            .Select(x => new TodoCategory(int.Parse(x.Element("Id").Value), x.Element("Name").Value));
        var addedTodoItem = xdoc.Root.Elements("TodoItem")
            .Where(x => int.Parse(x.Element("Id").Value) == todoItem.Id)
            .Select(x => new TodoItem
            {
                Id = int.Parse(x.Element("Id").Value),
                Title = x.Element("Title").Value,
                IsDone = bool.Parse(x.Element("IsDone").Value),
                CreatedAt = DateTime.Parse(x.Element("CreatedAt").Value),
                CompletedAt = DateTime.TryParse(x.Element("CompletedAt")?.Value, out DateTime tempDate)
                    ? tempDate
                    : null,
                Category = x.Element("CategoryId") != null && int.TryParse(x.Element("CategoryId").Value, out int categoryId)
                    ? todoCategories.FirstOrDefault(c => c.Id == categoryId)
                    : null
            })
            .FirstOrDefault();

        return addedTodoItem;
    }

    public void Update(TodoItem todoItem)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoItemElement = xdoc.Root.Elements("TodoItem")
            .Where(x => int.Parse(x.Element("Id").Value) == todoItem.Id)
            .FirstOrDefault();
        todoItemElement.Element("Title").Value = todoItem.Title;
        todoItemElement.Element("IsDone").Value = todoItem.IsDone.ToString();
        todoItemElement.Element("CreatedAt").Value = todoItem.CreatedAt.ToString();
        todoItemElement.Element("CompletedAt").Value = todoItem.CompletedAt.ToString();
        todoItemElement.Element("CategoryId").Value = todoItem.Category != null ? todoItem.Category.Id.ToString() : null;
        xdoc.Save(xmlPath);
    }

    public void Delete(int id)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoItemElement = xdoc.Root.Elements("TodoItem")
            .Where(x => int.Parse(x.Element("Id").Value) == id)
            .FirstOrDefault();
        todoItemElement.Remove();
        xdoc.Save(xmlPath);
    }
}