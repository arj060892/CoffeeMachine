using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.Service
{
    public interface IDrink
    {
        public Task<string> MakeDrinkAsync();
        public DrinkType DrinkType { get; }

    }
}