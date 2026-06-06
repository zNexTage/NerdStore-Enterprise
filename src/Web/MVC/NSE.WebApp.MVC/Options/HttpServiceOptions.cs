namespace NSE.WebApp.MVC.Options
{
    public class HttpServiceOptions
    {
        public HttpService AuthService { get; set; }
    }

    public class HttpService
    {
        public string BaseAddress { get; set; }
    }
}
