using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.ImplementationFactory;
using CoffeeMachine.Application.Exception;
using CoffeeMachine.Console.Constanst;
using CoffeeMachine.Domain.Types;
using System;
using System.Threading.Tasks;

namespace CoffeeMachineConsole.UI
{
    public class MakeCoffee
    {
        private readonly IImplementationFactory _implementationFactory;
        private readonly IConsole _consoleWrapper;

        public MakeCoffee(IImplementationFactory implementationFactory, IConsole consoleWrapper)
        {
            _implementationFactory = implementationFactory;
            _consoleWrapper = consoleWrapper;
        }

        public async Task StartMachine()
        {
            Console.WriteLine(UIText.WelcomeMessage);
            string inputKey;
            do
            {
                try
                {
                    Console.WriteLine(UIText.MenuOptions);
                    DrinkType drinkType = (DrinkType)Convert.ToInt32(_consoleWrapper.ReadLine());
                    var userDrinkOption = _implementationFactory.Create(drinkType);
                    Console.WriteLine(await userDrinkOption.MakeDrinkAsync());
                }
                catch (InvalidDrinkException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong ", ex.Message);
                }

                Console.WriteLine("\n*********************\nOrder Again? [Y/Off]\n*********************");
                inputKey = _consoleWrapper.ReadLine();
            } while (inputKey.ToLower() != "off");
        }
    }
}