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
    public class CustomerControllerTest
    {
        private readonly Fixture _fixture;

        public CustomerControllerTest()
        {
            if (_fixture == null)
            {
                this._fixture = new Fixture();
                this._fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                this._fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [TestCase(12)]
        public async Task Get_WhenCustomerExist_ReturnCustomer(int id)
        {
            var customer = this._fixture.Create<Customer>();
            var service = new Mock<ICustomerService>();
            var mapper = new Mock<IMapper>();
            var controller = new CustomerController(service.Object, mapper.Object);
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customer);

            var result = await controller.Get(id);
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                okObject.Value.Should().Be(customer);
            }
        }

        [Test]
        public async Task GetAll_WhenGetCollection_ReturnAllCustomers()
        {
            var service = new Mock<ICustomerService>();
            var mapper = new Mock<IMapper>();
            var customer1 = this._fixture.Create<Customer>();
            var customer2 = this._fixture.Create<Customer>();
            var customers = new List<Customer> { customer1, customer2 };
            var controller = new CustomerController(service.Object, mapper.Object);
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);

            var result = await controller.GetAll();
            var okObject = result as OkObjectResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.GetAllAsync(), Times.Once());
                okObject.Value.Should().Be(customers);
            }
        }

        [Test]
        public async Task Create_CallAddAsync()
        {
            var service = new Mock<ICustomerService>();
            var mapper = new Mock<IMapper>();
            var controller = new CustomerController(service.Object, mapper.Object);
            var customerModel = this._fixture.Create<CustomerModel>();
            var customer = this._fixture.Create<Customer>();
            mapper.Setup(x => x.Map<Customer>(customerModel)).Returns(customer);

            var result = controller.Create(customerModel);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                mapper.Verify(x => x.Map<Customer>(customerModel), Times.Once());
                service.Verify(x => x.AddAsync(customer), Times.Once());
            }
        }

        [Test]
        public async Task Update_WhenCustomerExist_CallTryUpdateAsync()
        {
            var service = new Mock<ICustomerService>();
            var mapper = new Mock<IMapper>();
            var controller = new CustomerController(service.Object, mapper.Object);
            var customerModel = this._fixture.Create<CustomerModel>();
            var id = this._fixture.Create<int>();
            var customer = this._fixture.Create<Customer>();
            mapper.Setup(x => x.Map<Customer>(customerModel)).Returns(customer);
            service.Setup(x => x.TryUpdateAsync(id, customer)).ReturnsAsync(true);

            var result = controller.Update(customerModel, id);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.TryUpdateAsync(id, customer), Times.Once());
            }
        }

        [Test]
        public async Task Delete_WhenCustomerExist_CallDeleteAsync()
        {
            var service = new Mock<ICustomerService>();
            var mapper = new Mock<IMapper>();
            var controller = new CustomerController(service.Object, mapper.Object);
            var id = this._fixture.Create<int>();
            var customer = this._fixture.Create<Customer>();
            service.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customer);

            var result = controller.Delete(id);
            var okObject = await result as OkResult;

            using (new AssertionScope())
            {
                okObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
                service.Verify(x => x.DeleteAsync(customer), Times.Once());
            }
        }
    }
}