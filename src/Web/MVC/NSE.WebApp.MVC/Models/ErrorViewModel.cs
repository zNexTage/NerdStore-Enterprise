namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErrorCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseResult
    {
        public string Title { get; set; } = default!;
        public int Status { get; set; }
        public ResponseErrorMessage Errors { get; set; } = new();
    }

    public class ResponseErrorMessage
    {
        public IEnumerable<string> Messages { get; set; } = new List<string>();
    }
}
