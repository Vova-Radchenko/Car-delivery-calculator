namespace CarDeliveryCalculator.WebAPI.Models
{
    public class RouteModel
    {
        public CityModel StartOfRoute { get; set; }
        public CityModel EndOfRoute { get; set; }
    }
}
