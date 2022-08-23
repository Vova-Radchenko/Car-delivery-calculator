using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await this._customerService.GetAllAsync();
            return this.Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var customer = await this._customerService.GetByIdAsync(id);
            if(customer == null)
            {
                return this.NotFound();
            }
            return this.Ok(customer);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CustomerModel customerModel)
        {
            var customer = this._mapper.Map<Customer>(customerModel);
            await this._customerService.AddAsync(customer);

            return this.Ok();
        }

        [HttpPut("{id:int}/edit")]
        public async Task<IActionResult> Update([FromBody] CustomerModel customerModel, [FromRoute] int id)
        {
            var customer = this._mapper.Map<Customer>(customerModel);
            var result = await this._customerService.TryUpdateAsync(id, customer);

            return result ? this.Ok() : this.NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var customer = await this._customerService.GetByIdAsync(id);
            if(customer == null)
            {
                return this.NotFound();
            }

            await this._customerService.DeleteAsync(customer);
            return this.Ok();
        }
    }
}