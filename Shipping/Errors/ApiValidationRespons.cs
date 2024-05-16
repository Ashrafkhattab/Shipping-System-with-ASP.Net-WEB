namespace Shipping.Errors
{
    public class ApiValidationRespons: ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationRespons():base(400)
        {
            
        }
    }
}
