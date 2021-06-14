using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;

namespace CoffeeMachine.Application.Contracts.ImplementationFactory
{
    /// <summary>
    /// Helps in generating objects for concrete implementaions
    /// </summary>
    public interface IImplementationFactory
    {
        /// <summary>
        /// Create objects for specific drink type based on User selection
        /// </summary>
        /// <param name="drinkType">Type of Drink selected by user. typeof DrinkType</param>
        /// <returns>Concrete Class for IDrink Interface</returns>
        IDrink Create(DrinkType drinkType);
    }
}