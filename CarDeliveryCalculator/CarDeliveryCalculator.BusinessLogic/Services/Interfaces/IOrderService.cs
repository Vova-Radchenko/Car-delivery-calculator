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
        Task<ICollection<Order>> GetAllForCustomer(int customerId);
        Task<Order> GetByIdAsync(int id);
        Task DeleteAsync(Order order);
        Task<bool> TryUpdateAsync(int id, Order order);
        Task FillOrderByIdAsync(Order order, int customerId, int carId, int routeId);
        Task<double> CalculateCostAsync(City city1, City city2, Car car);
    }
}