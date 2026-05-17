using Microsoft.AspNetCore.Mvc;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected static string ControllerName<T>() where T : Controller
        {
            return typeof(T).Name.Replace("Controller", string.Empty);
        }
    }
}
