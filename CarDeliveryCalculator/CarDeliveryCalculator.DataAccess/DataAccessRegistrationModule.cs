using Autofac;
using CarDeliveryCalculator.DataAccess.Repositories;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using CarDeliveryCalculator.DataAccess.Repositories.Implementation;
using System;

namespace CarDeliveryCalculator.DataAccess
{
    public class DataAccessRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarDeliveryDataContext>().AsSelf().SingleInstance();
            builder.RegisterType<CarRepository>().As<ICarRepository>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<RouteRepository>().As<IRouteRepository>();
        }
    }
}