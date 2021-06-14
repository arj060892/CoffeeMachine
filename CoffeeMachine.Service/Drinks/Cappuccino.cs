using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Service.Base;
using System;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Cappuccino : MachineAbstract, IDrink
    {
        private readonly Drink _drinkProp;
        private readonly IConsole _consoleWrapper;

        public Cappuccino(IMachineRepo machineRepo, IConsole consoleWrapper) : base(machineRepo)
        {
            _drinkProp = new()
            {
                BeanCount = 5,
                SugarCount = 0,
                DrinkType = Domain.Types.DrinkType.Cappuccino,
                MilkCount = 3
            };
            _consoleWrapper = consoleWrapper;
        }

        public Drink DrinkProp
        {
            get => _drinkProp;
        }

        public async Task<string> MakeDrinkAsync()
        {
            Console.WriteLine("Enter Sugar Cube Required[Number] :");
            var userInput = Convert.ToInt32(_consoleWrapper.ReadLine());
            _drinkProp.SugarCount = userInput;
            drinkToMake = DrinkProp;
            if (await IsBeanAvailable() && await IsMilkAvailable())
            {
                return await DispatchDrink();
            }
            return string.Join("\n", warningMessage);
        }
    }
}