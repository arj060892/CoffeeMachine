using CoffeeMachine.Application.Contracts.ImplementationFactory;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Application.Exception;
using CoffeeMachine.Domain.Types;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachine.Service.Drinks.Factory
{
    public class ImplementationFactory : IImplementationFactory
    {
        private readonly IEnumerable<IDrink> _drinks;

        public ImplementationFactory(IEnumerable<IDrink> drinks)
        {
            _drinks = drinks;
        }

        public IDrink Create(DrinkType drinkType)
        {
            var drinkInstance = _drinks.ToList().Find(e => e.DrinkType == drinkType);
            if (drinkInstance is null)
            {
                throw new InvalidDrinkException("\n==========================\nInvalid Selection\n===============================");
            }
            return drinkInstance;
        }
    }
}