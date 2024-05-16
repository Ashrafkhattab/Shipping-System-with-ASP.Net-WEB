namespace Shipping.MiddlWares
{
    public class ExceptionLogic:Exception
    {
        public string Message { get; set; }

        public ExceptionLogic(string message)
        {
            Message = message;
        }
    }
}
