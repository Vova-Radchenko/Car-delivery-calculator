using CarDeliveryCalculator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace CarDeliveryCalculator.DataAccess.Repositories
{
    public class CarDeliveryDataContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Route> Routes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(nameof(CarDeliveryDataContext));
            base.OnConfiguring(optionsBuilder);
        }
    }
}