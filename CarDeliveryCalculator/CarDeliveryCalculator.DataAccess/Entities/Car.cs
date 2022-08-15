using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int EngineCapacity { get; set; }
        public int Weight { get; set; }
    }
}
