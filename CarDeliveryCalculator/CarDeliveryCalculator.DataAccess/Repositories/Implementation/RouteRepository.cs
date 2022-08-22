using System;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class RouteRepository : IRouteRepository
    {
        private CarDeliveryDataContext _context;

        public RouteRepository(CarDeliveryDataContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Route route)
        {
            await this._context.Routes.AddAsync(route);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Route route)
        {
            this._context.Routes.Remove(route);
            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<Route>> GetAllAsync() 
            => await this._context.Routes.ToListAsync();

        public async Task<Route> GetByIdAsync(int id)
            => await this._context.Routes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Route route)
        {
            this._context.Routes.Update(route);
            await this._context.SaveChangesAsync();
        }
    }
}