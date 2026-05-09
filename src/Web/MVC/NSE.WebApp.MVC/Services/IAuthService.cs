using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<string> Login(UserLogin userLogin);

        Task<string> Register(UserRegister userRegister);
    }
}
