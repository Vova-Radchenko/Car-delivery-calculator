using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<ICollection<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}