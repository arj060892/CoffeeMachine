using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Exception
{
    public class InvalidDrinkException : SystemException
    {
        public InvalidDrinkException(string message)
           : base(message)
        {
        }
    }
}
