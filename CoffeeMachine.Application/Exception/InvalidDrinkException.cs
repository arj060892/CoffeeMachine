using System;

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