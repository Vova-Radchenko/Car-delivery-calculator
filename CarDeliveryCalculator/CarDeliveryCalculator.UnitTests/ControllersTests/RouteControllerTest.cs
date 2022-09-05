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
    public class RouteControllerTest
    {
        private readonly Fixture _fixture;

        public RouteControllerTest()
        {
            if (_fixture == null)
            {
                this._fixture = new Fixture();
                this._fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                this._fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [TestCase(12)]
        public async Task Get_WhenRouteExist_ReturnRoute(int id)
        {
            var route = this._fixture.Create<Route>();
            var service = new Mock<IRouteService>();
            var cityService = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new RouteController(service.Object, mapper.Object, cityService.Object);
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(route);

            var result = await controller.Get(id);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                okObject.Value.Should().Be(route);
            }
        }

        [Test]
        public async Task GetAll_WhenGetCollection_ReturnAllRoutes()
        {
            var service = new Mock<IRouteService>();
            var cityService = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var route1 = this._fixture.Create<Route>();
            var route2 = this._fixture.Create<Route>();
            var routes = new List<Route> { route1, route2 };
            var controller = new RouteController(service.Object, mapper.Object, cityService.Object);
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(routes);

            var result = await controller.GetAll();
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetAllAsync(), Times.Once());
                okObject.Value.Should().Be(routes);
            }
        }

        [Test]
        public async Task Create_CallAddAsync()
        {
            var service = new Mock<IRouteService>();
            var cityService = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new RouteController(service.Object, mapper.Object, cityService.Object);
            var routeModel = this._fixture.Create<RouteModel>();
            var route = this._fixture.Create<Route>();
            var startOfRoute = this._fixture.Create<City>();
            var endOfRoute = this._fixture.Create<City>();
            mapper.Setup(x => x.Map<Route>(routeModel)).Returns(route);
            cityService.Setup(x => x.GetByIdAsync(routeModel.StartOfRouteId)).ReturnsAsync(startOfRoute);
            cityService.Setup(x => x.GetByIdAsync(routeModel.EndOfRouteId)).ReturnsAsync(endOfRoute);

            var result = controller.Create(routeModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                mapper.Verify(x => x.Map<Route>(routeModel), Times.Once());
                cityService.VerifyAll();
                service.Verify(x => x.AddAsync(route), Times.Once());
            }
        }

        [Test]
        public async Task Update_WhenRouteExist_CallTryUpdateAsync()
        {
            var service = new Mock<IRouteService>();
            var cityService = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new RouteController(service.Object, mapper.Object, cityService.Object);
            var routeModel = this._fixture.Create<RouteModel>();
            var id = this._fixture.Create<int>();
            var route = this._fixture.Create<Route>();
            mapper.Setup(x => x.Map<Route>(routeModel)).Returns(route);
            service.Setup(x => x.TryUpdateAsync(id, route)).ReturnsAsync(true);

            var result = controller.Update(id, routeModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.TryUpdateAsync(id, route), Times.Once());
            }
        }

        [Test]
        public async Task Delete_WhenRouteExist_CallDeleteAsync()
        {
            var service = new Mock<IRouteService>();
            var cityService = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new RouteController(service.Object, mapper.Object, cityService.Object);
            var id = this._fixture.Create<int>();
            var route = this._fixture.Create<Route>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(route);

            var result = controller.Delete(id);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.DeleteAsync(route), Times.Once());
            }
        }
    }
}