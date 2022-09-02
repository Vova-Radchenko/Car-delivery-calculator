using AutoFixture;
using AutoFixture.NUnit3;
using AutoMapper;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.WebAPI.Controllers;
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
        [Test]
        [AutoData]
        public async Task GetTest(int id, Car car)
        {            
            //var fixture = new Fixture();
            //var car = fixture.Create<Car>();
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
    }
}