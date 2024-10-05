using Microsoft.AspNetCore.Mvc;

namespace ASPProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersAuthorization _userService;

        public UserController()
        {
            _userService = new UsersAuthorization();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Login and password are required.";
                return View();
            }

            _userService.Register(login, password);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (_userService.Login(login, password)) 
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid login or password.";
            return View();
        }
    }
}
