using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private CarDeliveryDataContext _context;

        public OrderRepository(CarDeliveryDataContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Order order)
        {
            await this._context.Orders.AddAsync(order);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            this._context.Orders.Remove(order);
            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllAsync() 
            => await this._context.Orders.ToListAsync();

        public Task<Order> GetByIdAsync(int id) 
            => this._context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Order order)
        {
            this._context.Orders.Update(order);
            await this._context.SaveChangesAsync();
        }
    }
}