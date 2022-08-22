using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Services.Interfaces
{
    public interface ICustomerService
    {
        Task AddAsync(Customer customer);
        Task<ICollection<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task DeleteAsync(Customer customer);
        Task<bool> TryUpdateAsync(int id, Customer customer);
    }
}