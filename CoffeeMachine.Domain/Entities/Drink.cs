using CoffeeMachine.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Domain.Entities
{
    public class Drink
    {
        public int SugarCount { get; set; }
        public int BeanCount { get; set; }
        public int MilkCount { get; set; }
        public DrinkType DrinkType{ get; set; }
    }
}
