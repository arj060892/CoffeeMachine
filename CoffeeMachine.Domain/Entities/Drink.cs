using CoffeeMachine.Domain.Types;

namespace CoffeeMachine.Domain.Entities
{
    public class Drink
    {
        public int SugarCount { get; set; }
        public int BeanCount { get; set; }
        public int MilkCount { get; set; }
        public DrinkType DrinkType { get; set; }
    }
}