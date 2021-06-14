using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;
using CoffeeMachine.Service.Base;
using System;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public class Cappuccino : MachineAbstract, IDrink
    {
        private readonly IConsole _consoleWrapper;

        public Cappuccino(IMachineRepo machineRepo, IConsole consoleWrapper) : base(machineRepo)
        {
            drinkToMake = new()
            {
                BeanCount = 5,
                SugarCount = 0,
                DrinkType = DrinkType.Cappuccino,
                MilkCount = 3
            };
            _consoleWrapper = consoleWrapper;
        }

        public DrinkType DrinkType => DrinkType.Cappuccino;

        public async Task<string> MakeDrinkAsync()
        {
            if (!(await IsBeanAvailable() && await IsMilkAvailable()))
            {
                return warningMessage;
            }
            Console.WriteLine("Enter Sugar Cube Required[Number] :");
            var userInput = Convert.ToInt32(_consoleWrapper.ReadLine());
            drinkToMake.SugarCount = userInput;
            return await DispatchDrink();
        }
    }
}