namespace CarDeliveryCalculator.WebAPI.Models
{
    public class RouteModel
    {
        public int StartOfRouteId { get; set; }
        public CityModel StartOfRoute { get; set; }
        public int EndOfRouteId { get; set; }
        public CityModel EndOfRoute { get; set; }
    }
}