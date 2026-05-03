using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register() => View();

        [HttpPost]
        [Route("nova-conta")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);


            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);


            return View();
        }

        [HttpGet]
        [Route("sair")]
        public ActionResult Logout() {
            return RedirectToAction("Index", nameof(HomeController).Replace("Controller", ""));
        }
    }
}
