using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.WorkWithCoordinates
{
    public interface ICoordinates
    {
        Task<Coordinate> GetCoordinatesAsync(City city);
        Task<string> GetCountryCode(string countryName);
    }
}