using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService(HttpClient client) : Service, IAuthService
    {
        private readonly HttpClient _client = client;

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var content = JsonSerializer.Serialize(userLogin);

            var loginContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/login")
            {
                Content = loginContent
            };
            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(httpRequest);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            if (!HandleResponse(response))
            {
                return new UserLoginResponse()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var content = JsonSerializer.Serialize(userRegister);

            var loginContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post, 
                "api/auth/nova-conta")
            {
                Content = loginContent
            };

            var response = await _client.SendAsync(httpRequest);

            if (!HandleResponse(response))
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                var loginResponse = new UserLoginResponse()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };

                return loginResponse;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserLoginResponse>(responseContent);
        }
    }
}
