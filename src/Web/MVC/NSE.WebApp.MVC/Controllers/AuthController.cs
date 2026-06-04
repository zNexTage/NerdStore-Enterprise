using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Controllers
{

    public class AuthController : MainController
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

            if (!response.IsValidResponse()) return ProcessInvalidResponse(response, userRegister);

            await Authenticate(response);

            return RedirectToAction("Index", ControllerName<HomeController>());
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

            if (!response.IsValidResponse()) return ProcessInvalidResponse(response, userLogin);

            await Authenticate(response);

            return RedirectToAction("Index", ControllerName<HomeController>());
        }

        [HttpGet]
        [Route("sair")]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", ControllerName<HomeController>());
        }


        private async Task Authenticate(UserLoginResponse loginResponse)
        {
            var token = GetFormatedToken(loginResponse.AccessToken);

            var claims = new List<Claim>
            {
                new Claim("JWT", loginResponse.AccessToken),
            };

            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddSeconds(loginResponse.ExpiresIn),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
                );


        }

        private static JwtSecurityToken GetFormatedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler()
                .ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
