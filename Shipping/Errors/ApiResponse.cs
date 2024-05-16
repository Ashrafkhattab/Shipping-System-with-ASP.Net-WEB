
namespace Shipping.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string?  Message { get; set; }
        public ApiResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode; 
            Message = message?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "BadRequest",
                401 => "UnAuthorized",
                404 => "Resorce was not found",
                500 => "Errors are the path to the dark side",
                _ => null
            };
        }
    }
}
