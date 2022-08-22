using Autofac;
using CarDeliveryCalculator.DataAccess.Repositories;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
using CarDeliveryCalculator.DataAccess.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess
{
    public class DataAccessRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarDeliveryDataContext>().AsSelf();
            builder.RegisterType<CarRepository>().As<ICarRepository>();
        }
    }
}
