using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Route Route { get; set; }
        public int Price { get; set; }
    }
}
