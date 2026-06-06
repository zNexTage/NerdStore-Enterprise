using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService(HttpClient client) : Service, IAuthService
    {
        private readonly HttpClient _client = client;

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent = Serialize(userLogin);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/login")
            {
                Content = loginContent
            };
            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(httpRequest);


            if (!HandleResponse(response))
            {
                return await Deserialize<UserLoginResponse>(response);
            }

            return await Deserialize<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var loginContent = Serialize(userRegister);

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                "api/auth/nova-conta")
            {
                Content = loginContent
            };

            var response = await _client.SendAsync(httpRequest);

            if (!HandleResponse(response))
            {
                var loginResponse = await Deserialize<UserLoginResponse>(response);

                return loginResponse;
            }

            return await Deserialize<UserLoginResponse>(response);
        }
    }
}
