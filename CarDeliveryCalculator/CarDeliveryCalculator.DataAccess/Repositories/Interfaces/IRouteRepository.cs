using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        Task AddAsync(Route route);
        Task<ICollection<Route>> GetAllAsync();
        Task<Route> GetByIdAsync(int id);
        Task UpdateAsync(Route route);
        Task DeleteAsync(Route route);
    }
}