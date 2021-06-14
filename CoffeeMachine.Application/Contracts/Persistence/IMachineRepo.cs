using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.Persistence
{
    /// <summary>
    ///    IMachineRepo represents a base interface which can be used to
    ///     query different DataSources , like InMemory,SqlServer etc. IMachineRepo follows Repository pattern.
    /// </summary>
    public interface IMachineRepo
    {
        /// <summary>
        /// Will create the drink and decrease the resources used to make the drink from inventory like sugar,beans and milk
        /// </summary>
        /// <param name="drink">Composition of the requested drink like sugar,beans,milk and the type of drink (coffee,latte)</param>
        /// <returns>string with Success message</returns>
        Task<string> MakeDrinkAsync(Drink drink);

        /// <summary>
        /// Gets the remaining quantiy of specific inventory
        /// </summary>
        /// <param name="inventoryType">Type of Inventory to be queried. typeof InventoryType</param>
        /// <returns>queired Inventory with it's specifications</returns>
        Task<Inventory> GetInventoryByTypeAsync(InventoryType inventoryType);

        /// <summary>
        /// Checks if any specific inventory is running low compared to the defined threshold
        /// </summary>
        /// <param name="inventoryType">Type of Inventory to be queried. typeof InventoryType</param>
        /// <param name="inventoryUnit">Defined Threshold for specific inventory</param>
        /// <returns>true or false , whether inventory is running low</returns>
        Task<bool> GetInventoryShortageAsync(InventoryType inventoryType, int inventoryUnit);
    }
}