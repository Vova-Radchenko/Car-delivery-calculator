using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Services.Interfaces
{
    public interface ICityService
    {
        Task AddAsync(City city);
        Task<ICollection<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task DeleteAsync(City city);
        Task<bool> TryUpdateAsync(int id, City city);
    }
}