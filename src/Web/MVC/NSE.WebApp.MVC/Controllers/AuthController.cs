using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register() => View();

        [HttpPost]
        [Route("nova-conta")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authService.Register(userRegister);

            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authService.Login(userLogin);

            return View();
        }

        [HttpGet]
        [Route("sair")]
        public ActionResult Logout() {
            return RedirectToAction("Index", nameof(HomeController).Replace("Controller", ""));
        }
    }
}
