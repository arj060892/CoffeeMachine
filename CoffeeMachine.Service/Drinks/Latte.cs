using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Service.Base;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Latte : MachineAbstract, IDrink
    {
        private readonly Drink _drinkProp;
        public Latte(IMachineRepo machineRepo) : base(machineRepo)
        {
            _drinkProp = new()
            {
                BeanCount = 3,
                SugarCount = 0,
                DrinkType = Domain.Types.DrinkType.Latte,
                MilkCount = 2
            };
        }

        public Drink DrinkProp
        {
            get => _drinkProp;
        }

        public async Task<string> MakeDrinkAsync()
        {
            drinkToMake = DrinkProp;
            if (await IsBeanAvailable() && await IsMilkAvailable())
            {
                return await DispatchDrink();

            }
            return string.Join("\n", warningMessage);
        }
    }
}