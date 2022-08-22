using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class CarRepository : ICarRepository
    {
        private CarDeliveryDataContext _context;

        public CarRepository(CarDeliveryDataContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Car car)
        {
            await this._context.Cars.AddAsync(car);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            this._context.Cars.Remove(car);
            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<Car>> GetAllAsync() 
            => await this._context.Cars.ToListAsync();

        public async Task<Car> GetByIdAsync(int id) 
            => await this._context.Cars.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Car car)
        {
            this._context.Cars.Update(car);
            await this._context.SaveChangesAsync();
        }
    }
}