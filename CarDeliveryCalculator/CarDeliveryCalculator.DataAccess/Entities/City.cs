using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.DataAccess.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[NotMapped]
        //public virtual ICollection<Route> Routes { get; set; }
    }
}