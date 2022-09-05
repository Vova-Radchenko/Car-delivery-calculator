using AutoFixture;
using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Controllers;
using CarDeliveryCalculator.WebAPI.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.UnitTests.ControllersTests
{
    public class OrderControllerTest
    {
        private readonly Fixture _fixture;

        public OrderControllerTest()
        {
            if (_fixture == null)
            {
                this._fixture = new Fixture();
                this._fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                this._fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [TestCase(12)]
        public async Task Get_WhenOrderExist_ReturnOrder(int id)
        {
            var order = this._fixture.Create<Order>();
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var controller = new OrderController(service.Object, mapper.Object);
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(order);

            var result = await controller.Get(id);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                okObject.Value.Should().Be(order);
            }
        }

        [Test]
        public async Task GetAll_WhenGetCollection_ReturnAllOrders()
        {
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var order1 = this._fixture.Create<Order>();
            var order2 = this._fixture.Create<Order>();
            var customerId = this._fixture.Create<int>();
            var orders = new List<Order> { order1, order2 };
            var controller = new OrderController(service.Object, mapper.Object);
            service.Setup(x => x.GetAllForCustomer(customerId)).ReturnsAsync(orders);

            var result = await controller.GetAll(customerId);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetAllForCustomer(customerId), Times.Once());
                okObject.Value.Should().Be(orders);
            }
        }

        [Test]
        public async Task CalculateCost_CallCalculateCostAsync()
        {
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var controller = new OrderController(service.Object, mapper.Object);
            var order = this._fixture.Create<Order>();
            var cost = this._fixture.Create<int>();
            var id = this._fixture.Create<int>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(order);
            service.Setup(x => x.CalculateCostAsync(order.Route.StartOfRoute, order.Route.EndOfRoute, order.Car)).ReturnsAsync(cost);

            var result = controller.CalculateCost(id);
            var okObject = await result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetByIdAsync(id), Times.Once());
                service.Verify(x => x.CalculateCostAsync(order.Route.StartOfRoute, order.Route.EndOfRoute, order.Car), Times.Once());
            }
        }

        [Test]
        public async Task Create_CallAddAsync()
        {
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var controller = new OrderController(service.Object, mapper.Object);
            var orderModel = this._fixture.Create<OrderModel>();
            var order = this._fixture.Create<Order>();
            mapper.Setup(x => x.Map<Order>(orderModel)).Returns(order);

            var result = controller.Create(orderModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                mapper.Verify(x => x.Map<Order>(orderModel), Times.Once());
                service.Verify(x => x.FillOrderByIdAsync(order, orderModel.CustomerId, orderModel.CarId, orderModel.RouteId), Times.Once());
                service.Verify(x => x.AddAsync(order), Times.Once());
            }
        }

        [Test]
        public async Task Update_WhenOrderExist_CallTryUpdateAsync()
        {
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var controller = new OrderController(service.Object, mapper.Object);
            var orderModel = this._fixture.Create<OrderModel>();
            var id = this._fixture.Create<int>();
            var order = this._fixture.Create<Order>();
            mapper.Setup(x => x.Map<Order>(orderModel)).Returns(order);
            service.Setup(x => x.TryUpdateAsync(id, order)).ReturnsAsync(true);

            var result = controller.Update(id, orderModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.TryUpdateAsync(id, order), Times.Once());
            }
        }

        [Test]
        public async Task Delete_WhenOrderExist_CallDeleteAsync()
        {
            var service = new Mock<IOrderService>();
            var mapper = new Mock<IMapper>();
            var controller = new OrderController(service.Object, mapper.Object);
            var id = this._fixture.Create<int>();
            var order = this._fixture.Create<Order>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(order);

            var result = controller.Delete(id);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.DeleteAsync(order), Times.Once());
            }
        }
    }
}