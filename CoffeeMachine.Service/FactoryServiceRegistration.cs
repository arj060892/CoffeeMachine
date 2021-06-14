using CoffeeMachine.Application.Contracts.ApplicationHelper;
using CoffeeMachine.Application.Contracts.ImplementationFactory;
using CoffeeMachine.Application.Contracts.Persistence;
using CoffeeMachine.Application.Contracts.Service;
using CoffeeMachine.Persistence.Repositories.InMemory;
using CoffeeMachine.Service.ApplicationHelper;
using CoffeeMachine.Service.Drinks.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoffeeMachine.Service
{
    public static class FactoryServiceRegistration
    {
        public static IServiceCollection AddFactoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IMachineRepo, InMemoryData>();
            services.AddSingleton<IImplementationFactory, ImplementationFactory>();
            services.AddSingleton<IConsole, ConsoleWrapper>();

            // for extended use we can do assembly registration for adding other drinks
            services.TryAddEnumerable(new[]
            {
               ServiceDescriptor.Scoped<IDrink,Coffee>(),
               ServiceDescriptor.Scoped<IDrink,Cappuccino>(),
               ServiceDescriptor.Scoped<IDrink,Latte>()
            });

            //uncomment to use SQL
            // services.AddSingleton<IMachineRepo, SqlData>();
            return services;
        }
    }
}