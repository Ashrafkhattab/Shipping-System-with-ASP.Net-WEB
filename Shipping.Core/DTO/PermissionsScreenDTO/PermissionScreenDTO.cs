namespace Shipping.DTO
{
    public class PermissionScreenDTO
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
        public bool Get { get; set; } 
        public bool Add { get; set; } 
        public bool Update { get; set; } 
        public bool Delete { get; set; } 
    }
}
