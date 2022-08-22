using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;

namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;

        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Route route)
            => this._repository.AddAsync(route);

        public Task DeleteAsync(Route route)
            => this._repository.DeleteAsync(route);

        public Task<ICollection<Route>> GetAllAsync()
            => this._repository.GetAllAsync();

        public Task<Route> GetByIdAsync(int id)
            => this._repository.GetByIdAsync(id);

        public async Task<bool> TryUpdateAsync(int id, Route route)
        {
            var routeToUpdate = await this._repository.GetByIdAsync(id);
            if (routeToUpdate != null)
            {
                routeToUpdate.StartOfRoute = route.StartOfRoute;
                routeToUpdate.EndOfRoute = route.EndOfRoute;

                await this._repository.UpdateAsync(routeToUpdate);

                return true;
            }

            return false;
        }
    }
}