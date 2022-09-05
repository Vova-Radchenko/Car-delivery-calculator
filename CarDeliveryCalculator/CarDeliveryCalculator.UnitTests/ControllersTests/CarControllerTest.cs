using AutoFixture;
using AutoFixture.NUnit3;
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
using System.Net;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.UnitTests.ControllersTests
{
    public class CarControllerTest
    {
        private readonly Fixture _fixture;

        public CarControllerTest()
        {
            if(_fixture == null)
            {
                this._fixture = new Fixture();
                this._fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                this._fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [TestCase(12)]
        public async Task Get_WhenCarExist_ReturnCar(int id)
        {
            var car = this._fixture.Create<Car>();
            var service = new Mock<ICarService>();
            var mapper = new Mock<IMapper>();
            var controller = new CarController(service.Object, mapper.Object);
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(car);

            var result = await controller.Get(id);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                okObject.Value.Should().Be(car);
            }
        }

        [Test]
        public async Task GetAll_WhenGetCollection_ReturnAllCar()
        {
            var service = new Mock<ICarService>();
            var mapper = new Mock<IMapper>();
            var car1 = this._fixture.Create<Car>();
            var car2 = this._fixture.Create<Car>();
            var cars = new List<Car> { car1, car2 };
            var controller = new CarController(service.Object, mapper.Object);
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(cars);

            var result = await controller.GetAll();
            var okObject = result as OkObjectResult;

            using(new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetAllAsync(), Times.Once());
                okObject.Value.Should().Be(cars);
            }
        }

        [Test]
        public async Task Create_CallAddAsync()
        {
            var service = new Mock<ICarService>();
            var mapper = new Mock<IMapper>();
            var controller = new CarController(service.Object, mapper.Object);
            var carModel = this._fixture.Create<CarModel>();
            var car = this._fixture.Create<Car>();
            mapper.Setup(x => x.Map<Car>(carModel)).Returns(car);

            var result = controller.Create(carModel);
            var okObject = await result as OkResult;

            using(new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                mapper.Verify(x => x.Map<Car>(carModel), Times.Once());
                service.Verify(x => x.AddAsync(car), Times.Once());
            }
        }

        [Test]
        public async Task Update_WhenCarExist_CallTryUpdateAsync()
        {
            var service = new Mock<ICarService>();
            var mapper = new Mock<IMapper>();
            var controller = new CarController(service.Object, mapper.Object);
            var carModel = this._fixture.Create<CarModel>();
            var id = this._fixture.Create<int>();
            var car = this._fixture.Create<Car>();
            mapper.Setup(x => x.Map<Car>(carModel)).Returns(car);
            service.Setup(x => x.TryUpdateAsync(id, car)).ReturnsAsync(true);

            var result = controller.Update(carModel, id);
            var okObject = await result as OkResult;

            using(new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.TryUpdateAsync(id, car), Times.Once());
            }
        }

        [Test]
        public async Task Delete_WhenCarExist_CallDeleteAsync()
        {
            var service = new Mock<ICarService>();
            var mapper = new Mock<IMapper>();
            var controller = new CarController(service.Object, mapper.Object);
            var id = this._fixture.Create<int>();
            var car = this._fixture.Create<Car>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(car);

            var result = controller.Delete(id);
            var okObject = await result as OkResult;

            using(new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.DeleteAsync(car), Times.Once());
            }
        }
    }
}