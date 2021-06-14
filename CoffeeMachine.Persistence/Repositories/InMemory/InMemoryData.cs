using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachine.Persistence.Repositories.InMemory
{
    public class InMemoryData : IMachineRepo
    {
        private readonly List<Inventory> _inventories;

        public InMemoryData()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=Guid.NewGuid(),
                    InventoryType = InventoryType.Beans,
                    InitalUnit = 25,
                    UnitRemaining = 25
                },
                 new Inventory {
                    InventoryId=Guid.NewGuid(),
                    InventoryType =InventoryType.Milk,
                    InitalUnit = 20,
                    UnitRemaining = 20
                },
                  new Inventory {
                    InventoryId=Guid.NewGuid(),
                    InventoryType = InventoryType.Sugar,
                    InitalUnit = 25,
                    UnitRemaining = 25
                }
            };
        }

        public async Task<List<Inventory>> GetInventoryAsync()
        {
            return await Task.FromResult(_inventories);
        }

        public async Task<Inventory> GetInventoryByTypeAsync(InventoryType inventoryType)
        {
            return await Task.FromResult(_inventories.Find(e => e.InventoryType == inventoryType));
        }

        public async Task<bool> GetInventoryShortageAsync(InventoryType inventoryType, int inventoryUnit)
        {
            return await Task.FromResult(_inventories.Find(e => e.InventoryType == inventoryType).UnitRemaining <= inventoryUnit);
        }

        public async Task<string> MakeDrinkAsync(Drink drink)
        {
            _inventories.ForEach(e =>
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

            return await Task.FromResult($"\n================================================\nYour {drink.DrinkType} is ready\n================================================\n\n");
        }
    }
}