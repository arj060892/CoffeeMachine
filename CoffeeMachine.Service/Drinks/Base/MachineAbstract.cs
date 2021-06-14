using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using System.Threading.Tasks;

namespace CoffeeMachine.Service.Base
{
    public abstract class MachineAbstract
    {
        public Drink drinkToMake;
        public string warningMessage;
        private readonly IMachineRepo _machineRepo;
        private readonly int _beanThreshold;

        public MachineAbstract(IMachineRepo machineRepo)
        {
            _machineRepo = machineRepo;
            _beanThreshold = 5; // get value from confirguration
        }

        public async Task<bool> IsBeanAvailable()
        {
            var beanQnty = await _machineRepo.GetInventoryByTypeAsync(InventoryType.Beans);
            var isBeanAvailable = beanQnty.UnitRemaining >= drinkToMake.BeanCount;
            if (!isBeanAvailable)
            {
                warningMessage = $"Machine is running low on Beans to make {drinkToMake.DrinkType}";
            }
            return isBeanAvailable;
        }

        public async Task<bool> IsMilkAvailable()
        {
            var milkQnty = await _machineRepo.GetInventoryByTypeAsync(InventoryType.Milk);
            var isMilkAvailable = milkQnty.UnitRemaining >= drinkToMake.MilkCount;
            if (!isMilkAvailable)
            {
                warningMessage = $"Machine is running low on Milk to make {drinkToMake.DrinkType}";
            }
            return isMilkAvailable;
        }

        public async Task<string> DispatchDrink()
        {
            var successMessage = await _machineRepo.MakeDrinkAsync(drinkToMake);
            if (await IsBeanShortage())
            {
                successMessage += "\n** Machine is running low on Beans **\n";
            }
            return successMessage;
        }

        public async Task<bool> IsBeanShortage()
        {
            return await _machineRepo.GetInventoryShortageAsync(InventoryType.Beans, _beanThreshold);
        }
    }
}