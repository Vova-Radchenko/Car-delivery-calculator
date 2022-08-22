using System;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDeliveryCalculator.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private CarDeliveryDataContext _context;

        public CustomerRepository(CarDeliveryDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await this._context.Customers.AddAsync(customer);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            this._context.Customers.Remove(customer);
            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<Customer>> GetAllAsync() 
            => await this._context.Customers.ToListAsync();

        public async Task<Customer> GetByIdAsync(int id) 
            => await this._context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Customer customer)
        {
            this._context.Customers.Update(customer);
            await this._context.SaveChangesAsync();
        }
    }
}