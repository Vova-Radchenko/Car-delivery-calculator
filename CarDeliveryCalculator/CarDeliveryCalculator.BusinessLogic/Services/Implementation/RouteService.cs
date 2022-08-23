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
            this._repository = repository;
        }

        public async Task AddAsync(Route route)
            =>  await this._repository.AddAsync(route);

        public async Task DeleteAsync(Route route)
            => await this._repository.DeleteAsync(route);

        public async Task<ICollection<Route>> GetAllAsync()
            => await this._repository.GetAllAsync();

        public async Task<Route> GetByIdAsync(int id)
            => await this._repository.GetByIdAsync(id);

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