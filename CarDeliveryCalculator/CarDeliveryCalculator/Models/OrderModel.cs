namespace CarDeliveryCalculator.WebAPI.Models
{
    public class OrderModel
    {
        public CustomerModel Customer { get; set; }
        public CarModel Car { get; set; }
        public RouteModel Route { get; set; }
        public int Price { get; set; }
    }
}
