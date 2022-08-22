using CarDeliveryCalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Services.Interfaces
{
    public interface IRouteService
    {
        Task AddAsync(Route route);
        Task<ICollection<Route>> GetAllAsync();
        Task<Route> GetByIdAsync(int id);
        Task DeleteAsync(Route route);
        Task<bool> TryUpdateAsync(int id, Route route);
    }
}