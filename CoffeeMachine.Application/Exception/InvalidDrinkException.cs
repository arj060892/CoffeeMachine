using System;

namespace CoffeeMachine.Application.Exception
{
    /// <summary>
    /// Used to handle custom exception when user will select invalid drink type which is not present in the system
    /// </summary>
    public class InvalidDrinkException : SystemException
    {
        public InvalidDrinkException(string message)
           : base(message)
        {
        }
    }
}