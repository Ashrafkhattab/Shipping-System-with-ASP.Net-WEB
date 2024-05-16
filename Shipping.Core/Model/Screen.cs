namespace Shipping.Core.Model
{
    public class Screen : ModelBase
    {
        
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public ICollection<ScreenPermission> ScreenPermission { get; set;}
    }
}
