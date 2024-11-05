using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Utils;

namespace MvcTodoApp.Controllers;

public class StorageTypeController : Controller
{
    [HttpPost]
    public IActionResult SetStorageType(string storageType)
    {
        HttpContext.Session.SetString(SessionConstants.StorageType, storageType);
        return RedirectToAction("Index", "Home");
    }
}