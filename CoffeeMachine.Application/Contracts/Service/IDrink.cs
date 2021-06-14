using CoffeeMachine.Domain.Types;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.Service
{
    /// <summary>
    /// IDrink defines root level abstraction for all the type of Drinks the system can generate
    /// </summary>
    public interface IDrink
    {
        /// <summary>
        /// Will create the drink and update inventory via IMachineRepo and checks if beans are running short or not
        /// </summary>
        /// <returns>string with Success message and warning for beans shortage</returns>
        public Task<string> MakeDrinkAsync();

        /// <summary>
        /// Defines the type of Drink user requested
        /// </summary>
        public DrinkType DrinkType { get; }
    }
}