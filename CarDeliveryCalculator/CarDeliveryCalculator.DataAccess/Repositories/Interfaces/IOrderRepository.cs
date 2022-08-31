using System;
using CarDeliveryCalculator.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<ICollection<Order>> GetAllAsync();
        Task<ICollection<Order>> GetAllForCustomer(int customerId);
        Task<Order> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}