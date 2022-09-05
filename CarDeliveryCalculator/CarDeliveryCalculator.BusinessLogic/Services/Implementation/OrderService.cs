using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using GeoCoordinatePortable;
using CarDeliveryCalculator.BusinessLogic.WorkWithCoordinates;
using CarDeliveryCalculator.BusinessLogic.Exceptions;

namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ICoordinates _coordinates;

        public OrderService(IOrderRepository repository, ICoordinates coordinates)
        {
            this._repository = repository;
            this._coordinates = coordinates;
        }

        public async Task AddAsync(Order order)
            => await this._repository.AddAsync(order);

        public async Task DeleteAsync(Order order)
            => await this._repository.DeleteAsync(order);

        public async Task<ICollection<Order>> GetAllAsync()
            => await this._repository.GetAllAsync();

        public async Task<ICollection<Order>> GetAllForCustomer(int customerId) 
            => await this._repository.GetAllForCustomer(customerId);

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

        public async Task FillOrderByIdAsync(Order order, int customerId, int carId, int routeId)
        {
            var customer = await this._repository.GetCustomerById(customerId);
            var car = await this._repository.GetCarById(carId);
            var route = await this._repository.GetRouteById(routeId);

            if (customer == null || car == null || route == null)
                throw new IncorrectIdException("Wrong id!");

            order.Customer = customer;
            order.Car = car;
            order.Route = route;
        }

        public async Task<double> CalculateCostAsync(City city1, City city2, Car car)
        {            
            var city1Coordinate = await this._coordinates.GetCoordinatesAsync(city1);
            var city2Coordinate = await this._coordinates.GetCoordinatesAsync(city2);

            var pin1 = new GeoCoordinate(city1Coordinate.latitude, city1Coordinate.longitude);
            var pin2 = new GeoCoordinate(city2Coordinate.latitude, city2Coordinate.longitude);

            var distance = pin1.GetDistanceTo(pin2) / 1000;

            var cost = distance * 45 
                * (car.Weight > 2000 ? 1.5 : 1) 
                * (car.Year < 2012 ? 1.35 : 1.05)
                * (car.EngineCapacity/1000 == 0 ? 1 : car.EngineCapacity / 1000);

            return Math.Round(cost, 2);
        }
    }
}