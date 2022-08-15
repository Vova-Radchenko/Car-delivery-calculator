using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public City StartOfRoute { get; set; }
        public City EndOfRoute { get; set; }
    }
}
