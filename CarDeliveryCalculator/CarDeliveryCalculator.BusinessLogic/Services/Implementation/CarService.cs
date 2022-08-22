using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;

namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Car car)
            => this._repository.AddAsync(car);

        public Task DeleteAsync(Car car)
            => _repository.DeleteAsync(car);

        public Task<ICollection<Car>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<Car> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public async Task<bool> TryUpdateAsync(int id, Car car)
        {
            var carToUpdate = await this._repository.GetByIdAsync(id);
            if (carToUpdate != null)
            {
                carToUpdate.Manufacture = car.Manufacture;
                carToUpdate.Model = car.Model;
                carToUpdate.Year = car.Year;
                carToUpdate.EngineCapacity = car.EngineCapacity;
                carToUpdate.Weight = car.Weight;

                await this._repository.UpdateAsync(carToUpdate);

                return true;
            }
            return false;
        }
    }
}