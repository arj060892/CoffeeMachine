using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Application.Exception;
using CoffeeMachine.Domain.Types;
using CoffeeMachine.Service;
using CoffeeMachine.Service.Drinks.Factory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeMachine.Infra.Test
{
    public class ImplementationFactoryShould
    {
        private Mock<IEnumerable<IDrink>> _mockIEnum;
        private List<IDrink> _mockList;

        public ImplementationFactoryShould()
        {
             _mockIEnum = new Mock<IEnumerable<IDrink>>();
             _mockList = new List<IDrink>()
            {
                new Coffee(null, null),
                new Cappuccino(null,null),
                new Latte(null)
            };
            _mockIEnum.Setup(x => x.GetEnumerator()).Returns(_mockList.GetEnumerator());

        }
        [Fact]
        public void RetrunTypeOfCoffee()
        {            
            var drinkType = DrinkType.Coffee;
            var sut = new ImplementationFactory(_mockIEnum.Object);
            Assert.IsType<Coffee>(sut.Create(drinkType));
        }

        [Fact]
        public void RetrunTypeOfCappuccino()
        {
            var drinkType = DrinkType.Cappuccino;
            var sut = new ImplementationFactory(_mockIEnum.Object);
            Assert.IsType<Cappuccino>(sut.Create(drinkType));
        }

        [Fact]
        public void RetrunTypeOfLatte()
        {
            var drinkType = DrinkType.Latte;
            var sut = new ImplementationFactory(_mockIEnum.Object);
            Assert.IsType<Latte>(sut.Create(drinkType));
        }

        [Fact]
        public void ThrowErrorIfInvalidParamPassed()
        {
            var drinkType = DrinkType.Latte; // Latte is removed from test to force throw error
            _mockList = new List<IDrink>()
            {
                new Coffee(null, null),
            };
            _mockIEnum.Setup(x => x.GetEnumerator()).Returns(_mockList.GetEnumerator());
            var sut = new ImplementationFactory(_mockIEnum.Object);
            Assert.Throws<InvalidDrinkException>(()=>sut.Create(drinkType));
        }
    }
}
