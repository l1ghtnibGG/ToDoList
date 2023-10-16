#nullable enable
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ToDo_Domain_Entities;
using ToDoList_Mvc_UI.Models;
using ToDoList_Mvc_UI.Models.Repo;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace ToDoList_Mvc_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserRepository _userRepository;
        
        public HomeController(IToDoListRepository repository, IUserRepository userRepository)
        {
            _toDoListRepository = repository;
            _userRepository = userRepository;
        }
        
        
        [Microsoft.AspNetCore.Mvc.HttpPost("/Registration")]
        public async Task<IActionResult> Registration([FromForm] UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.Users.FirstOrDefault(x => 
                        x.Email == userRegistration.EmailAddress) == null)
                {
                    return await CreateUser(new User
                    {
                        Email = userRegistration.EmailAddress,
                        Name = userRegistration.Name,
                        Password = userRegistration.Password
                    });
                }

                return RedirectToAction("Error", new { message = 
                    "User already exist. Try again or log in." });
            }

            return View(userRegistration);
        }

        private async Task<IActionResult> CreateUser(User user)
        {
            var createdUser = _userRepository.AddUser(user);

            if (createdUser == null)
                return RedirectToAction("Error", new { message = "User wasn't created" });

            await UserSignIn(createdUser);
            return RedirectToAction("Index", new { Id = createdUser.Id });
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/")]
        [Microsoft.AspNetCore.Mvc.HttpGet("/Login")]
        public IActionResult Login() => View();

        [Microsoft.AspNetCore.Mvc.HttpPost("/Login")]
        public async Task<IActionResult> Login([FromForm] UserLogIn userLogIn)
        {
            if (!ModelState.IsValid)
                return View(userLogIn);

            var user = Authenticate(userLogIn);
            
            if(user == null)
                return RedirectToAction("Error", new {message = 
                        "Wrong email or password. Try again or register."});

            await UserSignIn(user);
            return RedirectToAction("Index", new { Id = user.Id });
        }

        private User? Authenticate(UserLogIn userLogin) => _userRepository.
            Users.FirstOrDefault(x => x.Email == userLogin.EmailAddress && 
                                          x.Password == userLogin.Password);
        
        private async Task UserSignIn(User user)
        {
            var claims = GetClaims(user);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
        }
        
        private List<Claim> GetClaims(User user) => new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

        [Microsoft.AspNetCore.Mvc.HttpGet("/Registration")]
        public IActionResult Registration() => View();
        
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("User/{Id:guid}")]
        public IActionResult Index(Guid Id) => View(_toDoListRepository.ToDoLists.
            Where(x => x.UserId == Id));

        [Microsoft.AspNetCore.Mvc.HttpPost("/User/{id}")]
        public IActionResult AddItem(string id, [FromForm] ToDoList toDoList)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new { Id = Guid.Parse(User.Claims.FirstOrDefault().Value) });

            return _toDoListRepository.AddItem(toDoList) == null ? 
                RedirectToAction("Error", new { message = "Wrong input" }) : 
                RedirectToAction("Index", new { Id = toDoList.UserId });
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/User/{userId}/EditPanel/{id}")]
        public IActionResult EditPage(string userId, string id, [FromForm]ToDoList toDoList)
        {
            toDoList.Id = Convert.ToInt64(id);
            return View(toDoList);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/User/{userId}/EditPanel")]
        public IActionResult UpdateItem(string userId, [FromForm] ToDoList toDoList)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("EditPage",
                    new { userId = userId, id = toDoList.Id.ToString(), toDoList = toDoList });

            return _toDoListRepository.UpdateItem(toDoList) == null
                ? RedirectToAction("Error", new { message = "Wrong input" })
                : RedirectToAction("Index", new { Id = toDoList.UserId });
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/Delete")]
        public IActionResult DeleteItem(long id, string userId) => _toDoListRepository.DeleteItem(id) == 0 ? 
            RedirectToAction("Error", new { message = "Delete is went wrong" }) : 
            RedirectToAction("Index", new {Id = Guid.Parse(userId)});

        [Microsoft.AspNetCore.Mvc.HttpPost("/ToggleIsDone")]
        public IActionResult ToggleIsDone(long id, string userId) => _toDoListRepository.UpdateDoneItem(id) == 0 ? 
            RedirectToAction("Error", new { message = "User isn't found" }) : 
            RedirectToAction("Index", new {Id = Guid.Parse(userId)});

        [Microsoft.AspNetCore.Mvc.HttpGet("/LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("/Error")]
        public IActionResult Error(string message) => View("Error", message);
    }
}
