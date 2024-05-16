namespace Shipping.DTO
{
    public class PermissionScreensRequestDTO
    {
        public string RoleName { get; set; }
        public List<PermissionScreenDTO> PermissionScreens { get; set; }
    }
}
