using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task AddAsync(Car car);
        Task<ICollection<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
    }
}
