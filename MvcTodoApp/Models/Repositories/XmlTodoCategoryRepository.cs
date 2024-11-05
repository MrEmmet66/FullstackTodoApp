using System.Xml.Linq;
using MvcTodoApp.Models.Entities;

namespace MvcTodoApp.Models.Repositories;

public class XmlTodoCategoryRepository : ITodoCategoryRepository
{
    private readonly string xmlPath = null;
    
    public XmlTodoCategoryRepository(string xmlPath)
    {
        this.xmlPath = xmlPath;
        FileInfo fileInfo = new FileInfo(xmlPath);
        if (!fileInfo.Exists)
        {
            XDocument xdoc = new XDocument(new XElement("TodoCategories"));
            xdoc.Save(xmlPath);
        }
    }
    public List<TodoCategory> GetTodoCategories()
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoCategories = xdoc.Root.Elements("TodoCategory")
            .Select(x => new TodoCategory
            {
                Id = int.Parse(x.Element("Id").Value),
                Name = x.Element("Name").Value
            });
        return todoCategories.ToList();
    }

    public TodoCategory GetTodoCategory(int id)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var todoCategory = xdoc.Root.Elements("TodoCategory")
            .Where(x => int.Parse(x.Element("Id").Value) == id)
            .Select(x => new TodoCategory
            {
                Id = int.Parse(x.Element("Id").Value),
                Name = x.Element("Name").Value
            });
        return todoCategory.FirstOrDefault();
    }

    public void Add(TodoCategory todoCategory)
    {
        
        XDocument xdoc = XDocument.Load(xmlPath);
        var categories = xdoc.Root.Elements("TodoCategory");
        int maxId = categories.Any() ? categories.Max(x => int.Parse(x.Element("Id").Value)) : 0;
        todoCategory.Id = maxId + 1;
        XElement todoCategoryElement = new XElement("TodoCategory",
            new XElement("Id", todoCategory.Id),
            new XElement("Name", todoCategory.Name));
        xdoc.Root.Add(todoCategoryElement);
        xdoc.Save(xmlPath);
    }

    public void Update(TodoCategory todoCategory)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        XElement todoCategoryElement = xdoc.Root.Elements("TodoCategory")
            .Where(x => int.Parse(x.Element("Id").Value) == todoCategory.Id)
            .FirstOrDefault();
        todoCategoryElement.Element("Name").Value = todoCategory.Name;
        xdoc.Save(xmlPath);
    }

    public void Delete(int id)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        XElement todoCategoryElement = xdoc.Root.Elements("TodoCategory")
            .Where(x => int.Parse(x.Element("Id").Value) == id)
            .FirstOrDefault();
        todoCategoryElement.Remove();
        xdoc.Save(xmlPath);
    }
}