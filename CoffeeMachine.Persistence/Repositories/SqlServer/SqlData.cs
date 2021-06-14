using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMachine.Persistence.Repositories.SqlServer
{
    public class SqlData : IMachineRepo
    {
        protected readonly CoffeeMachineDbContext _dbContext;

        public SqlData(CoffeeMachineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Inventory> GetInventoryByTypeAsync(InventoryType inventoryType)
        {
            return await _dbContext.Inventories.Where(e => e.InventoryType == inventoryType).FirstOrDefaultAsync();
        }

        public async Task<bool> GetInventoryShortageAsync(InventoryType inventoryType, int inventoryUnit)
        {
            var inventory = await _dbContext.Inventories
                .Where(e => e.InventoryType == inventoryType)
                .FirstOrDefaultAsync();
            return inventory.UnitRemaining <= inventoryUnit;
        }

        public async Task<string> MakeDrinkAsync(Drink drink)
        {
            var currentInventory = await _dbContext.Inventories.ToListAsync();
            currentInventory.ForEach(e =>
            {
                switch (e.InventoryType)
                {
                    case InventoryType.Beans:
                        e.UnitRemaining -= drink.BeanCount;
                        break;

                    case InventoryType.Sugar:
                        e.UnitRemaining -= drink.SugarCount;
                        break;

                    case InventoryType.Milk:
                        e.UnitRemaining -= drink.MilkCount;
                        break;
                }
            });
            _dbContext.SaveChanges();

            return await Task.FromResult($"Your {nameof(drink.DrinkType)} is ready");
        }
    }
}