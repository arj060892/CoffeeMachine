using CoffeeMachine.Domain.Types;
using System;

namespace CoffeeMachine.Domain.Entities
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public InventoryType InventoryType { get; set; }
        public int UnitRemaining { get; set; }
        public int InitalUnit { get; set; }
    }
}