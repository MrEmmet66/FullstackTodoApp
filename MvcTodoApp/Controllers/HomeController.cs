using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Diagnostics;
using MvcTodoApp.Models.Entities;
using MvcTodoApp.Models.Factories;
using MvcTodoApp.Models.ViewModels;
using MvcTodoApp.Utils;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryFactoryProvider repositoryFactoryProvider;

        public HomeController(IRepositoryFactoryProvider repositoryFactoryProvider)
        {
            this.repositoryFactoryProvider = repositoryFactoryProvider;
        }

        public IActionResult Index()
        {
            ITodoItemRepository todoItemRepository = repositoryFactoryProvider.CreateRepositoryFactory().CreateTodoItemRepository();
            ITodoCategoryRepository todoCategoryRepository = repositoryFactoryProvider.CreateRepositoryFactory().CreateTodoCategoryRepository();
            string storageType = HttpContext.Session.GetString(SessionConstants.StorageType);
            var todoItems = todoItemRepository.GetTodoItems().OrderBy(i => i.IsDone);
            var todoCategories = todoCategoryRepository.GetTodoCategories();
            MainPageViewModel pageViewModel = new MainPageViewModel(todoItems, todoCategories);
            pageViewModel.StorageType = storageType;
            return View(pageViewModel);
        }
        
        public IActionResult RemoveTodoItem(int id)
        {
            ITodoItemRepository todoItemRepository = repositoryFactoryProvider.CreateRepositoryFactory().CreateTodoItemRepository();
            todoItemRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateTodoItemStatus(int id, bool isDone)
        {
            ITodoItemRepository todoItemRepository = repositoryFactoryProvider.CreateRepositoryFactory().CreateTodoItemRepository();
            TodoItem todoItem = todoItemRepository.GetTodoItem(id);
            todoItem.IsDone = isDone;
            todoItemRepository.Update(todoItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddTodoItem(MainPageViewModel pageViewModel)
        {
            ITodoItemRepository todoItemRepository = repositoryFactoryProvider.CreateRepositoryFactory().CreateTodoItemRepository();
            if(pageViewModel.NewTodoItem.Category?.Id == 0)
            {
                pageViewModel.NewTodoItem.Category = null;
            }
            todoItemRepository.Add(pageViewModel.NewTodoItem);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}