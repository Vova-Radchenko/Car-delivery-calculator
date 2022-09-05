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
    public class CityControllerTest
    {
        private readonly Fixture _fixture;

        public CityControllerTest()
        {
            if (_fixture == null)
            {
                this._fixture = new Fixture();
                this._fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                this._fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [TestCase(12)]
        public async Task Get_WhenCityExist_ReturnCity(int id)
        {
            var city = this._fixture.Create<City>();
            var service = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new CityController(service.Object, mapper.Object);
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(city);

            var result = await controller.Get(id);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                okObject.Value.Should().Be(city);
            }
        }

        [Test]
        public async Task GetAll_WhenGetCollection_ReturnAllCities()
        {
            var service = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var city1 = this._fixture.Create<City>();
            var city2 = this._fixture.Create<City>();
            var cities = new List<City> { city1, city2 };
            var controller = new CityController(service.Object, mapper.Object);
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(cities);

            var result = await controller.GetAll();
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetAllAsync(), Times.Once());
                okObject.Value.Should().Be(cities);
            }
        }

        [Test]
        public async Task Create_CallAddAsync()
        {
            var service = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new CityController(service.Object, mapper.Object);
            var cityModel = this._fixture.Create<CityModel>();
            var city = this._fixture.Create<City>();
            mapper.Setup(x => x.Map<City>(cityModel)).Returns(city);

            var result = controller.Create(cityModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                mapper.Verify(x => x.Map<City>(cityModel), Times.Once());
                service.Verify(x => x.AddAsync(city), Times.Once());
            }
        }

        [Test]
        public async Task Update_WhenCityExist_CallTryUpdateAsync()
        {
            var service = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new CityController(service.Object, mapper.Object);
            var cityModel = this._fixture.Create<CityModel>();
            var id = this._fixture.Create<int>();
            var city = this._fixture.Create<City>();
            mapper.Setup(x => x.Map<City>(cityModel)).Returns(city);
            service.Setup(x => x.TryUpdateAsync(id, city)).ReturnsAsync(true);

            var result = controller.Update(id, cityModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.TryUpdateAsync(id, city), Times.Once());
            }
        }

        [Test]
        public async Task Delete_WhenCityExist_CallDeleteAsync()
        {
            var service = new Mock<ICityService>();
            var mapper = new Mock<IMapper>();
            var controller = new CityController(service.Object, mapper.Object);
            var id = this._fixture.Create<int>();
            var city = this._fixture.Create<City>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(city);

            var result = controller.Delete(id);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.DeleteAsync(city), Times.Once());
            }
        }
    }
}
