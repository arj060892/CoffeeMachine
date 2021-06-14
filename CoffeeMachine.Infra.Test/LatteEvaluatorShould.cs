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
    public class LatteEvaluatorShould : IDisposable
    {
        private readonly string _successMessage;
        private readonly string _lowBeansWarning;
        private readonly Mock<IMachineRepo> _mockRepo;
        private readonly Latte _sut; // System Under Test

        public LatteEvaluatorShould()
        {
            _mockRepo = new Mock<IMachineRepo>();
            _sut = new Latte(_mockRepo.Object);
            _successMessage = $"Your {DrinkType.Latte} is ready";
            _lowBeansWarning = "Machine is running low on Beans"; ;
        }

        [Fact]
        public async void ShowWarningIfBeanIsLess()
        {
            var beans = new Inventory { UnitRemaining = 1, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            await _sut.MakeDrinkAsync();

            Assert.NotNull(_sut.warningMessage);
            Assert.Contains("running low on Beans", _sut.warningMessage);
        }

        [Fact]
        public async void ShowWarningIfMilkIsLess()
        {
            var milk = new Inventory { UnitRemaining = 1, InventoryType = InventoryType.Milk };
            var beans = new Inventory { UnitRemaining = 6, InventoryType = InventoryType.Beans };
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Beans))
                .Returns(Task.FromResult(beans));
            _mockRepo.Setup(e => e.GetInventoryByTypeAsync(InventoryType.Milk))
                .Returns(Task.FromResult(milk));

            await _sut.MakeDrinkAsync();

            Assert.NotNull(_sut.warningMessage);
            Assert.Contains("running low on Milk", _sut.warningMessage);
        }

        [Fact]
        public async void ShowSuccessDispatch()
        {
            InitializeInventory();
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
            _mockRepo.Setup(e => e.MakeDrinkAsync(_sut.drinkToMake))
    .Returns(Task.FromResult(_successMessage));
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing Latte");
        }
    }
}