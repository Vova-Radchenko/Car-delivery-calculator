using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; 

namespace CarDeliveryCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            this._carService = carService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await this._carService.GetAllAsync();
            return this.Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var car = await this._carService.GetByIdAsync(id);
            if(car == null)
            {
                return this.NotFound();
            }
            return this.Ok(car);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarModel carModel)
        {
            var car = this._mapper.Map<Car>(carModel);
            await this._carService.AddAsync(car);

            return this.Ok();
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> Update([FromBody] CarModel carModel, int id)
        {
            var car = this._mapper.Map<Car>(carModel);
            var result = await this._carService.TryUpdateAsync(id, car);

            return result ? this.Ok() : this.NotFound();
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var car = await this._carService.GetByIdAsync(id);
            if(car != null)
            {
                await this._carService.DeleteAsync(car);

                return this.Ok();
            }

            return this.NotFound();
        }
    }
}