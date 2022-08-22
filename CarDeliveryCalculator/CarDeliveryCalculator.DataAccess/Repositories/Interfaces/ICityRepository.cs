using System;
using CarDeliveryCalculator.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task AddAsync(City city);
        Task<ICollection<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
    }
}
