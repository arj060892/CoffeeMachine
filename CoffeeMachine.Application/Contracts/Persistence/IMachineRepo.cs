using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.Persistence
{
    public interface IMachineRepo
    {
        Task<string> MakeDrinkAsync(Drink drink);

        Task<List<Inventory>> GetInventoryAsync();

        Task<Inventory> GetInventoryByTypeAsync(InventoryType inventoryType);

        Task<bool> GetInventoryShortageAsync(InventoryType inventoryType, int inventoryUnit);
    }
}