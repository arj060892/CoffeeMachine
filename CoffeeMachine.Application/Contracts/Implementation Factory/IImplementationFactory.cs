using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;

namespace CoffeeMachine.Application.Contracts.ImplementationFactory
{
    public interface IImplementationFactory
    {
        IDrink Create(DrinkType drinkType);
    }
}