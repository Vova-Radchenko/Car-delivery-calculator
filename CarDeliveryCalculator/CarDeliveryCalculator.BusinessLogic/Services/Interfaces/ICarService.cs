using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Services.Interfaces
{
    public interface ICarService
    {
        Task AddAsync(Car car);
        Task<ICollection<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task DeleteAsync(Car car);
        Task<bool> TryUpdateAsync(int id, Car car);
    }
}