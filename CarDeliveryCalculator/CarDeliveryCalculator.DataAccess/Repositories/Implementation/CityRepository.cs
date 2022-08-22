using System;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class CityRepository : ICityRepository
    {
        private CarDeliveryDataContext _context;

        public CityRepository(CarDeliveryDataContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(City city)
        {
            await this._context.Cities.AddAsync(city);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(City city)
        {
            this._context.Cities.Remove(city);
            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<City>> GetAllAsync() 
            => await this._context.Cities.ToListAsync();

        public async Task<City> GetByIdAsync(int id) 
            => await this._context.Cities.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(City city)
        {
            this._context.Cities.Update(city);
            await this._context.SaveChangesAsync();
        }
    }
}