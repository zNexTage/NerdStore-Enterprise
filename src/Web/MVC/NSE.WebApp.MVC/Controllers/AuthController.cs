using Microsoft.AspNetCore.Mvc;

namespace NSE.WebApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
