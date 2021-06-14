using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;
using CoffeeMachine.Service.Base;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Latte : MachineAbstract, IDrink
    {
        public Latte(IMachineRepo machineRepo) : base(machineRepo)
        {
            drinkToMake = new()
            {
                BeanCount = 3,
                SugarCount = 0,
                DrinkType = DrinkType.Latte,
                MilkCount = 2
            };
        }

        public DrinkType DrinkType => DrinkType.Latte;

        public async Task<string> MakeDrinkAsync()
        {
            return await IsBeanAvailable() && await IsMilkAvailable() ? await DispatchDrink() : warningMessage;
        }
    }
}