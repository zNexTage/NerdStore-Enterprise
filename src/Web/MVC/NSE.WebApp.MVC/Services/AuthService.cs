using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService(HttpClient client) : IAuthService
    {
        private readonly HttpClient _client = client;

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var content = JsonSerializer.Serialize(userLogin);

            var loginContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/login");
            httpRequest.Content = loginContent;
            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(httpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<UserLoginResponse>(responseContent, options);
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var content = JsonSerializer.Serialize(userRegister);

            var loginContent = new StringContent(content);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/nova-conta");
            httpRequest.Content = loginContent;
            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(httpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserLoginResponse>(responseContent);
        }
    }
}
