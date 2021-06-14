using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;
using CoffeeMachine.Service.Base;
using System;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Coffee : MachineAbstract, IDrink
    {
        private readonly IConsole _consoleWrapper;

        public Coffee(IMachineRepo machineRepo, IConsole consoleWrapper) : base(machineRepo)
        {
            drinkToMake = new()
            {
                BeanCount = 2,
                SugarCount = 0,
                DrinkType = DrinkType.Coffee,
                MilkCount = 0
            };
            _consoleWrapper = consoleWrapper;
        }

        public DrinkType DrinkType => DrinkType.Coffee;

        public async Task<string> MakeDrinkAsync()
        {
            if (!await IsBeanAvailable())
            {
                return warningMessage;
            }
            Console.WriteLine("Do you want milk? [Y/N]");
            var userInput = _consoleWrapper.ReadLine().ToUpper();
            bool isMilkRequired = userInput == "Y";
            drinkToMake.MilkCount = isMilkRequired ? 1 : 0;
            if (!isMilkRequired
                || isMilkRequired && await IsMilkAvailable()) // check for milk's availability only if milk is required
            {
                return await DispatchDrink();
            }

            return warningMessage;
        }
    }
}