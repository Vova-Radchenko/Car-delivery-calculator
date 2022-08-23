using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;

namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            this._repository = repository;
        }

        public async Task AddAsync(Order order)
            => await this._repository.AddAsync(order);

        public async Task DeleteAsync(Order order)
            => await this._repository.DeleteAsync(order);

        public async Task<ICollection<Order>> GetAllAsync()
            => await this._repository.GetAllAsync();

        public async Task<Order> GetByIdAsync(int id)
            => await this._repository.GetByIdAsync(id);

        public async Task<bool> TryUpdateAsync(int id, Order order)
        {
            var orderToUpdate = await this._repository.GetByIdAsync(id);
            if(orderToUpdate!= null)
            {
                orderToUpdate.Route = order.Route;
                orderToUpdate.Price = order.Price;
                orderToUpdate.Car = order.Car;
                orderToUpdate.Customer = order.Customer;

                await this._repository.UpdateAsync(orderToUpdate);

                return true;
            }

            return false;
        }
    }
}