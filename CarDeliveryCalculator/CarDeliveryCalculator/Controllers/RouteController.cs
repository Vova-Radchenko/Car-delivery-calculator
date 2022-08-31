using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public RouteController(IRouteService routeService,  IMapper mapper, ICityService cityService)
        {
            _routeService = routeService;
            _mapper = mapper;
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var routes = await this._routeService.GetAllAsync();
            return this.Ok(routes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var route = await this._routeService.GetByIdAsync(id);
            if(route == null)
            {
                return this.NotFound();
            }

            return this.Ok(route);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RouteModel routeModel)
        {
            var route = this._mapper.Map<Route>(routeModel);

            //var route = new Route();

            if (routeModel.StartOfRouteId != 0)
            {
                var startOfRoute = await this._cityService.GetByIdAsync(routeModel.StartOfRouteId);
                route.StartOfRoute = startOfRoute;
            }
            else
            {
                return this.BadRequest("Not specified \"startOfRouteId\"!");
            }

            if (routeModel.EndOfRouteId != 0)
            {
                var endOfRoute = await this._cityService.GetByIdAsync(routeModel.EndOfRouteId);
                route.EndOfRoute = endOfRoute;
            }
            else
            {
                return this.BadRequest("Not specified \"endOfRouteId\"!");
            }

            await this._routeService.AddAsync(route);

            return this.Ok();
        }

        [HttpPut("{id:int}/edit")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RouteModel routeModel)
        {
            var route = this._mapper.Map<Route>(routeModel);
            var result = await this._routeService.TryUpdateAsync(id, route);

            return result ? this.Ok() : this.NotFound();
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var route = await this._routeService.GetByIdAsync(id);
            if(route != null)
            {
                await this._routeService.DeleteAsync(route);

                return this.Ok();
            }

            return this.NotFound();
        }
    }
}