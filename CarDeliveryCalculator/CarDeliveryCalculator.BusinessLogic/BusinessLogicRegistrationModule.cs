using Autofac;
using CarDeliveryCalculator.DataAccess;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using CarDeliveryCalculator.BusinessLogic.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDeliveryCalculator.BusinessLogic.WorkWithCoordinates;

namespace CarDeliveryCalculator.BusinessLogic
{
    public class BusinessLogicRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccessRegistrationModule>();
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<RouteService>().As<IRouteService>();
            builder.RegisterType<Coordinates>().As<ICoordinates>();
        }
    }
}