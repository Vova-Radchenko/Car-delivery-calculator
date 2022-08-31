using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDeliveryCalculator.WebAPI.Models;
using CarDeliveryCalculator.DataAccess.Entities;

namespace CarDeliveryCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("{customerId:int}/get-all")]
        public async Task<IActionResult> GetAll([FromRoute] int customerId)
        {
            var orders = await this._orderService.GetAllForCustomer(customerId);
            return this.Ok(orders);
        }

        [HttpGet("{id:int}/get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var order = await this._orderService.GetByIdAsync(id);
            if(order == null)
            {
                return this.NotFound();
            }
            return this.Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderModel orderModel)
        {
            var order = this._mapper.Map<Order>(orderModel);
            await this._orderService.AddAsync(order);

            return this.Ok();
        }
    }
}