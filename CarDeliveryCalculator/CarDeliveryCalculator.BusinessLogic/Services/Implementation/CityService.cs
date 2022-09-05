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

        public async Task AddAsync(City city)
            => await this._repository.AddAsync(city);

        public async Task DeleteAsync(City city)
            => await this._repository.DeleteAsync(city);

        public async Task<ICollection<City>> GetAllAsync()
            => await this._repository.GetAllAsync();

        public async Task<City> GetByIdAsync(int id)
            => await this._repository.GetByIdAsync(id);

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