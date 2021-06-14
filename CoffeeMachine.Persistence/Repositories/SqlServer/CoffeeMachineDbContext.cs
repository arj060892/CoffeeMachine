using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Types;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeMachine.Persistence.Repositories.SqlServer
{
    public class CoffeeMachineDbContext : DbContext
    {
        public CoffeeMachineDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoffeeMachineDbContext).Assembly);

            //seed data, added through migrations
            
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = Guid.Parse("{b191c99e-0497-4c1d-810e-100be869c5f8}"),
                InventoryType = InventoryType.Beans,
                InitalUnit = 25,
                UnitRemaining = 25
            });

            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = Guid.Parse("{c23c2e5d-fe8c-4c71-85e6-a3494087a309}"),
                InventoryType = InventoryType.Milk,
                InitalUnit = 20,
                UnitRemaining = 20
            });

            modelBuilder.Entity<Inventory>().HasData(new Inventory
             {
                 InventoryId = Guid.Parse("{a63acaf6-cc81-48fe-bf1b-e05a2ddb656b}"),
                 InventoryType = InventoryType.Sugar,
                 InitalUnit = 25,
                 UnitRemaining = 25
             });
        }
    }
}