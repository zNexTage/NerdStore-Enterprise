using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Diagnostics;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel()
            {
                RequestId = HttpContext.TraceIdentifier,
                ErrorCode = id
            };

            switch (id)
            {
                case 500:
                    {
                        modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                        modelErro.Title = "Ocorreu um erro!";
                        break;
                    }
                case 404:
                    {
                        modelErro.Message =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                        modelErro.Title = "Ops! Página não encontrada.";
                        break;
                    }
                case 403:
                    {
                        modelErro.Message = "Você não tem permissão para fazer isto.";
                        modelErro.Title = "Acesso Negado";
                        break;
                    }
                default: return StatusCode(404);
            }

            return View(modelErro);
        }
    }
}
