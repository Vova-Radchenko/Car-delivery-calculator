using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;

namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _repository;

        public CityService(ICityRepository repository)
        {
            this._repository = repository;
        }

        public Task AddAsync(City city)
            => this._repository.AddAsync(city);

        public Task DeleteAsync(City city)
            => this._repository.DeleteAsync(city);

        public Task<ICollection<City>> GetAllAsync()
            => this._repository.GetAllAsync();

        public Task<City> GetByIdAsync(int id)
            => this._repository.GetByIdAsync(id);

        public async Task<bool> TryUpdateAsync(int id, City city)
        {
            var cityToUpdate = await this._repository.GetByIdAsync(id);
            if(cityToUpdate != null)
            {
                cityToUpdate.Name = city.Name;

                await this._repository.UpdateAsync(cityToUpdate);

                return true;
            }

            return false;
        }
    }
}