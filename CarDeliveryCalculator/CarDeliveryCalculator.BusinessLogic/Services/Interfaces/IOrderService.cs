using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<ICollection<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task DeleteAsync(Order order);
        Task<bool> TryUpdateAsync(int id, Order order);
    }
}