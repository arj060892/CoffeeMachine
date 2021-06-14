using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using CoffeeMachine.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeMachine.Infra.Test
{
    public class CoffeeEvaluatorShould : IDisposable
    {
        private readonly string _successMessage;
        private readonly string _lowBeansWarning;
        private readonly Mock<IMachineRepo> _mockRepo;
        private readonly Mock<IConsole> _mockConsole;
        private readonly Coffee _sut; // System Under Test

        public CoffeeEvaluatorShould()
        {
            _mockRepo = new Mock<IMachineRepo>();
            _mockConsole = new Mock<IConsole>();
            _sut = new Coffee(_mockRepo.Object, _mockConsole.Object);
            _successMessage = $"Your {DrinkType.Coffee} is ready";
            _lowBeansWarning = "Machine is running low on Beans"; ;
        }

        [Fact]
        public async void ShowWarningIfBeanIsLess()
        {
            var beans = new Inventory { UnitRemaining = 1, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("N");
            await _sut.MakeDrinkAsync();

            Assert.NotNull(_sut.warningMessage);
            Assert.Contains("running low on Beans", _sut.warningMessage);
        }

        [Fact]
        public async void ShowWarningIfMilkIsLess()
        {
            var milk = new Inventory { UnitRemaining = 0, InventoryType = InventoryType.Milk };
            var beans = new Inventory { UnitRemaining = 6, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Milk))
                .Returns(Task.FromResult(milk));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("Y");

            await _sut.MakeDrinkAsync();

            Assert.NotNull(_sut.warningMessage);
            Assert.Contains("running low on Milk", _sut.warningMessage);
        }

        [Fact]
        public async void CheckForMilkIfRequired()
        {
            var beans = new Inventory { UnitRemaining = 6, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("N");

            await _sut.MakeDrinkAsync();

            Assert.Null(_sut.warningMessage);
        }

        [Fact]
        public async void ShowSuccessDispatchWithoutMilk()
        {
            InitializeInventoryWithoutMilk();
            _mockRepo.Setup(e => e.GetInventoryShortageAsync(InventoryType.Beans, 5))
                .Returns(Task.FromResult(false));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("N");

            var drinkDispatchedMessage = await _sut.MakeDrinkAsync();

            Assert.Null(_sut.warningMessage);
            Assert.Contains(_successMessage, drinkDispatchedMessage);
        }

        [Fact]
        public async void ShowSuccessDispatchWithWarningForWithoutMilk()
        {
            InitializeInventoryWithoutMilk();
            _mockRepo.Setup(e => e.GetInventoryShortageAsync(InventoryType.Beans, 5))
                .Returns(Task.FromResult(true));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("N");

            var drinkDispatchedMessage = await _sut.MakeDrinkAsync();

            Assert.Null(_sut.warningMessage);
            Assert.Contains(_lowBeansWarning, drinkDispatchedMessage);
        }

        [Fact]
        public async void ShowSuccessDispatch()
        {
            InitializeInventory();
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("Y");
            _mockRepo.Setup(e => e.GetInventoryShortageAsync(InventoryType.Beans, 5))
                .Returns(Task.FromResult(false));

            var drinkDispatchedMessage = await _sut.MakeDrinkAsync();

            Assert.Null(_sut.warningMessage);
            Assert.Contains(_successMessage, drinkDispatchedMessage);
        }

        [Fact]
        public async void ShowSuccessDispatchWithWarning()
        {
            InitializeInventory();
            _mockRepo.Setup(e => e.GetInventoryShortageAsync(InventoryType.Beans, 5))
                .Returns(Task.FromResult(true));
            _mockConsole.Setup(e => e.ReadLine())
                .Returns("Y");

            var drinkDispatchedMessage = await _sut.MakeDrinkAsync();

            Assert.Null(_sut.warningMessage);
            Assert.Contains(_lowBeansWarning, drinkDispatchedMessage);
        }

        private void InitializeInventory()
        {
            var milk = new Inventory { UnitRemaining = 5, InventoryType = InventoryType.Milk };
            var beans = new Inventory { UnitRemaining = 5, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Milk))
                .Returns(Task.FromResult(milk));
            _mockRepo.Setup(e => e.MakeDrinkAsync(_sut.DrinkProp))
                .Returns(Task.FromResult(_successMessage));
        }

        private void InitializeInventoryWithoutMilk()
        {
            var beans = new Inventory { UnitRemaining = 6, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockRepo.Setup(e => e.MakeDrinkAsync(_sut.DrinkProp))
                .Returns(Task.FromResult(_successMessage));
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing Coffee");
        }
    }
}
