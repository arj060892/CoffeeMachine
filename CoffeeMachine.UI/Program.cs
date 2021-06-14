using CoffeeMachine.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeMachineConsole.UI
{
    public class Program
    {
        public static void Main()
        {
            // initialize service injection of IServiceCollection
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            // start application
            _ = serviceProvider.GetService<MakeCoffee>().StartMachine();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddFactoryServices();
            services.AddSingleton<MakeCoffee>();
            return services;
        }
    }
}