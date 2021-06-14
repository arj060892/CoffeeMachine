using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Contracts.ImplementationFactory
{
    public interface IImplementationFactory
    {
        IDrink Create(DrinkType drinkType);
    }
}
