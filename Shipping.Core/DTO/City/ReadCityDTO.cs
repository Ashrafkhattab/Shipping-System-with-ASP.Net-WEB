namespace Shipping.DTO.City
{
    public class ReadCityDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Pickup { get; set; }
        public bool IsDeleted { get; set; }
    }
}
