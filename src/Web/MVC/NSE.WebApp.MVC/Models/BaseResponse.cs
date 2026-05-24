namespace NSE.WebApp.MVC.Models
{
    public class BaseResponse
    {
        public ResponseResult ResponseResult { get; set; } = new();

        public bool IsValidResponse() => ResponseResult != null && !ResponseResult.Errors.Messages.Any();
    }
}
