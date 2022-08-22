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
        }
    }
}
