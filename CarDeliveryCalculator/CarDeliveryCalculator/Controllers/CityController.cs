using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            this._cityService = cityService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await this._cityService.GetAllAsync();
            return this.Ok(cities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var city = await this._cityService.GetByIdAsync(id);
            if(city == null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CityModel cityModel)
        {
            var city = this._mapper.Map<City>(cityModel);
            await this._cityService.AddAsync(city);

            return this.Ok();
        }

        [HttpPut("{id:int}/edit")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CityModel cityModel)
        {
            var city = this._mapper.Map<City>(cityModel);
            var result = await this._cityService.TryUpdateAsync(id, city);

            return result ? this.Ok() : this.NotFound();
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var city = await this._cityService.GetByIdAsync(id);
            if(city == null)
            {
                return this.NotFound();
            }

            await this._cityService.DeleteAsync(city);
            return this.Ok();
        }
    }
}