using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDo_Domain_Entities;
using ToDoList_Mvc_UI.Models;
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

        [Route("/")]
        public IActionResult Index() => View(_repository.ToDoLists);

        public IActionResult AddItem([FromHeader] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _repository.AddItem(toDoList);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditPage([FromQuery]long id, [FromQuery] ToDoList toDoList)
        {
            toDoList.Id = id;
            return View(toDoList);
        }

        public IActionResult UpdateItem([FromHeader] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateItem(toDoList);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem([FromForm] long id)
        {
            _repository.DeleteItem(id);

            return RedirectToAction("Index");
        }

        public IActionResult ToggleIsDone([FromForm] long id)
        {
            _repository.UpdateDoneItem(id);

            return RedirectToAction("Index");
        }
    }
}
