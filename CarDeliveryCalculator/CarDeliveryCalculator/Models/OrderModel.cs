namespace CarDeliveryCalculator.WebAPI.Models
{
    public class OrderModel
    {
        public CustomerModel Customer { get; set; }
        public int CustomerId { get; set; }
        public CarModel Car { get; set; }
        public int CarId { get; set; }
        public RouteModel Route { get; set; }
        public int RouteId { get; set; }
        public double Price { get; set; }
    }
}
