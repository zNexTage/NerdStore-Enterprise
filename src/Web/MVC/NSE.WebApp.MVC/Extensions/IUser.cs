using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }

    public class AppUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string Name => _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
        public Guid GetUserId()
        {
            if (!IsAuthenticated()) return Guid.Empty;

            return Guid.Parse(_httpContextAccessor.HttpContext.User.GetUserId());
        }

        public string GetUserEmail()
        {
            if (!IsAuthenticated()) return string.Empty;

            return _httpContextAccessor.HttpContext.User.GetUserEmail();
        }
        public string GetUserToken()
        {
            if (!IsAuthenticated()) return string.Empty;

            return _httpContextAccessor.HttpContext.User.GetUserToken();
        }
        public bool IsAuthenticated() => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        public bool HasRole(string role) => _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
        public IEnumerable<Claim> GetClaims() => _httpContextAccessor.HttpContext?.User.Claims ?? Enumerable.Empty<Claim>();
        public HttpContext GetHttpContext() => _httpContextAccessor.HttpContext!;
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
