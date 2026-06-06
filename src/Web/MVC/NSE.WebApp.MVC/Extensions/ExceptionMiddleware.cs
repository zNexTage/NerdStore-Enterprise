using NSE.WebApp.MVC.Exceptions;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        private void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpException)
        {
            if(httpException.StatusCode== System.Net.HttpStatusCode.Unauthorized)
            {
                var returnUrl = context.Request.Path + context.Request.QueryString;

                context.Response.Redirect($"/login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                return;
            }

            context.Response.StatusCode = (int)httpException.StatusCode;
        }
    }
}
