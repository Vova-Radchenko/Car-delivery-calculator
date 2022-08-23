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
            this._repository = repository;
        }

        public async Task AddAsync(Car car)
            => await this._repository.AddAsync(car);

        public async Task DeleteAsync(Car car)
            => await _repository.DeleteAsync(car);

        public async Task<ICollection<Car>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Car> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

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