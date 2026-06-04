using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected static string ControllerName<T>() where T : Controller
        {
            return typeof(T).Name.Replace("Controller", string.Empty);
        }

        protected ActionResult ProcessInvalidResponse(BaseResponse baseResponse, object request)
        {
            var messages = baseResponse.ResponseResult.Errors.Messages;

            foreach (var msg in messages)
            {
                ModelState.AddModelError(string.Empty, msg);
            }

            return View(request);
        }

    }
}
