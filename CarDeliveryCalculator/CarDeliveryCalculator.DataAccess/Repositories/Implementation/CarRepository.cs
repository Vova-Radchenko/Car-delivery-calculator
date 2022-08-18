using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class CarRepository : ICarRepository
    {
        public Task AddAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Car>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
