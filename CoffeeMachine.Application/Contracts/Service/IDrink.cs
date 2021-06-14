using CoffeeMachine.Domain.Entities;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.Service
{
    public interface IDrink
    {
        public Task<string> MakeDrinkAsync();
        public Drink DrinkProp { get;}

    }
}