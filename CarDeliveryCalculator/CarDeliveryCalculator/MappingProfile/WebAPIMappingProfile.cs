using AutoMapper;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Models;

namespace CarDeliveryCalculator.WebAPI.MappingProfile
{
    public class WebAPIMappingProfile : Profile
    {
        public WebAPIMappingProfile()
        {
            this.CreateMap<Car, CarModel>();
            this.CreateMap<CarModel, Car>();
            this.CreateMap<City, CityModel>();
            this.CreateMap<CityModel, City>();
            this.CreateMap<Customer, CustomerModel>();
            this.CreateMap<CustomerModel, Customer>();
            this.CreateMap<Order, OrderModel>();
            this.CreateMap<OrderModel, Order>();
            this.CreateMap<Route, RouteModel>();
            this.CreateMap<RouteModel, Route>();
        }
    }
}