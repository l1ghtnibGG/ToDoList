using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList_Mvc_UI.Models.Repo;

namespace ToDoList_Mvc_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoListRepository _repository;

        public HomeController(IToDoListRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IActionResult Index() => View(_repository.ToDoLists.Where(x => x.IsDone == false));
    }
}
