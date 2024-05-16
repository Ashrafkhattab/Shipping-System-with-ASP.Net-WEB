namespace Shipping.DTO.City
{
    public class ShowCityDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Pickup { get; set; }
        public int GovernorateId { get; set; }
    }
}
