using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDeliveryCalculator.WebAPI.Models;
using CarDeliveryCalculator.DataAccess.Entities;
using System;

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

        [HttpGet("{id:int}/calculate-cost")]
        public async Task<IActionResult> CalculateCost([FromRoute] int id)
        {
            var order = await this._orderService.GetByIdAsync(id);
            var cost = this._orderService.CalculateCostAsync(order.Route.StartOfRoute, order.Route.EndOfRoute);

            return this.Ok(cost);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderModel orderModel)
        {
            var order = this._mapper.Map<Order>(orderModel);

            if(orderModel.CustomerId == 0 || orderModel.CarId == 0 || orderModel.RouteId == 0)            
                return this.BadRequest("One of the IDs of the order objects is not specified!");

            try
            {
                await this._orderService.FillOrderByIdAsync(order, orderModel.CustomerId, orderModel.CarId, orderModel.RouteId);
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
            await this._orderService.AddAsync(order);

            return this.Ok();
        }

        [HttpPut("{id:int}/edit")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderModel orderModel)
        {
            var order = this._mapper.Map<Order>(orderModel);
            var result = await this. _orderService.TryUpdateAsync(id, order);

            return result ? this.Ok() : this.NotFound();
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var order = await this._orderService.GetByIdAsync(id);
            if(order != null)
            {
                await this._orderService.DeleteAsync(order);

                return this.Ok();
            }

            return this.NotFound();
        }        
    }
}