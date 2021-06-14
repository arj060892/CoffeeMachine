using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Service.Base;
using System;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Coffee : MachineAbstract, IDrink
    {
        private readonly Drink _drinkProp;
        private readonly IConsole _consoleWrapper;

        public Coffee(IMachineRepo machineRepo, IConsole consoleWrapper) : base(machineRepo)
        {
            _drinkProp = new()
            {
                BeanCount = 2,
                SugarCount = 0,
                DrinkType = Domain.Types.DrinkType.Coffee,
                MilkCount = 0
            };
            _consoleWrapper = consoleWrapper;
        }

        public Drink DrinkProp
        {
            get => _drinkProp;
        }

        public async Task<string> MakeDrinkAsync()
        {
            Console.WriteLine("Do you want milk? [Y/N]");
            var userInput = _consoleWrapper.ReadLine().ToUpper();
            bool isMilkRequired = userInput == "Y";
            _drinkProp.MilkCount = isMilkRequired ? 1 : 0;
            drinkToMake = DrinkProp;

            if (await IsBeanAvailable() && !isMilkRequired 
                || await IsBeanAvailable() && isMilkRequired && await IsMilkAvailable()) // check for milk's availability only if milk is required
            {
               return await DispatchDrink();
            }

            return warningMessage;
        }
    }
}