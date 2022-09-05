using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDeliveryCalculator.BusinessLogic.Exceptions
{
    public class IncorrectIdException : Exception
    {
        public IncorrectIdException(string message) : base(message)
        {
        }
    }
}