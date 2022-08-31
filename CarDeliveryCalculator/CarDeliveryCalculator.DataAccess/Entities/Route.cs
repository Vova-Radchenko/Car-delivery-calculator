using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Entities
{
    public class Route
    {
        public int Id { get; set; }
        //[NotMapped]
        public virtual City StartOfRoute { get; set; }
        //[NotMapped]
        public virtual City EndOfRoute { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}