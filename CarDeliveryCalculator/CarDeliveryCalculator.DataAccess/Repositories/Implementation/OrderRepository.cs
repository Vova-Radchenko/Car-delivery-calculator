using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<ICollection<Order>> GetAllForCustomer(int customerId)
        {
            var customer = this._context.Customers.Where(x => x.Id == customerId);
            return await this._context.Orders.Where(x => x.Customer.Id == customerId).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id) 
            => await this._context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Order order)
        {
            this._context.Orders.Update(order);
            await this._context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerById(int id) 
            => await this._context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Car> GetCarById(int id)
            => await this._context.Cars.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Route> GetRouteById(int id)
            => await this._context.Routes.FirstOrDefaultAsync(x => x.Id == id);
    }
}