using CoffeeMachine.Application.Contracts.ApplicationHelper;
using System;

namespace CoffeeMachine.Service.ApplicationHelper
{
    public class ConsoleWrapper : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}