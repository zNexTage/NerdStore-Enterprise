using NSE.WebApp.MVC.Exceptions;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);
                case 400: return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected StringContent Serialize(object data)
        {
            return new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage httpResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}
