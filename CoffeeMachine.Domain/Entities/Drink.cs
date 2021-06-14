using CoffeeMachine.Domain.Types;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// Defines base Drink Type
    /// </summary>
    public class Drink
    {
        /// <summary>
        /// Amount of Sugar required to create a drink
        /// </summary>
        public int SugarCount { get; set; }

        /// <summary>
        /// Amount of Bean required to create a drink
        /// </summary>
        public int BeanCount { get; set; }

        /// <summary>
        /// Amount of Milk required to create a drink
        /// </summary>
        public int MilkCount { get; set; }

        /// <summary>
        /// Type of Drink
        /// </summary>
        public DrinkType DrinkType { get; set; }
    }
}