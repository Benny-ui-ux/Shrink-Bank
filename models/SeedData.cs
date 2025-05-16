using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shrink_Bank.Models;

namespace Shrink_Bank.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new InventoryContext(
            serviceProvider.GetRequiredService<DbContextOptions<InventoryContext>>());

        if (context.Inventories.Any())
        {
            return; 
        }

        context.Employees.AddRange(
            new Employee { Name = "Aiden", Password = "Aiden123", Department = "Frozen", Position ="Stocker"},
            new Employee { Name = "Holly", Password = "Holly123", Department = "Produce", Position = "Stocker"},
            new Employee { Name = "Weldon", Password = "Weldon123", Department = "Dairy", Position = "Stocker"},
            new Employee { Name = "Carmen", Password = "Carmen123", Department = "Dry-Goods", Position = "Stocker" }
        );

    

        List <Inventory> Inventories = new List<Inventory>
   {
       new Inventory { InventoryName = "Frozen Fries", Department= "Frozen", Price = 5.99M, Quantity = "15", ExpirationDate = DateTime.Parse("2025-12-15") },
       new Inventory { InventoryName = "Vanilla Ice Cream", Department= "Frozen", Price = 3.99M, Quantity = "7", ExpirationDate = DateTime.Parse("2026-01-12") },
       new Inventory { InventoryName = "Frozen Green Peas", Department= "Frozen", Price = 2.99M, Quantity = "20", ExpirationDate = DateTime.Parse("2026-05-17") },
       new Inventory { InventoryName = "Frozen Tamales", Department= "Frozen", Price = 7.99M, Quantity = "10", ExpirationDate = DateTime.Parse("2025-10-24") },
       new Inventory { InventoryName = "Frozen Breakfast Sandiwches", Department= "Frozen", Price = 9.99M, Quantity = "10", ExpirationDate = DateTime.Parse("2025-05-30") },
       new Inventory { InventoryName = "Bananas", Department= "Produce", Price = 0.925M, Quantity = "40", ExpirationDate = DateTime.Parse("2024-05-27") },
       new Inventory { InventoryName = "Navel Oranges", Department= "Produce", Price = 0.50M, Quantity = "50", ExpirationDate = DateTime.Parse("2025-06-24") },
       new Inventory { InventoryName = "Asparagus", Department= "Produce", Price = 2.00M, Quantity = "30", ExpirationDate = DateTime.Parse("2025-07-04") },
       new Inventory { InventoryName = "Kale", Department= "Produce", Price = 0.99M, Quantity = "15", ExpirationDate = DateTime.Parse("2025-05-15") },
       new Inventory { InventoryName = "Gala Apples", Department= "Produce", Price = 1.00M, Quantity = "17", ExpirationDate = DateTime.Parse("2024-05-15") },
       new Inventory { InventoryName = "Whipped Cream", Department= "Dairy", Price = 1.99M, Quantity = "20", ExpirationDate = DateTime.Parse("2025-05-27") },
       new Inventory { InventoryName = "One Dozen Eggs", Department= "Dairy", Price = 2999.99M, Quantity = "50", ExpirationDate = DateTime.Parse("2025-06-14") },
       new Inventory { InventoryName = "Half and Half", Department= "Dairy", Price = 3.99M, Quantity = "34", ExpirationDate = DateTime.Parse("2025-06-08") },
       new Inventory { InventoryName = "Greek Yogurt", Department= "Dairy", Price = 2.99M, Quantity = "18", ExpirationDate = DateTime.Parse("2025-05-30") },
       new Inventory { InventoryName = "Whole Milk", Department= "Dairy", Price = 2.99M, Quantity = "3", ExpirationDate = DateTime.Parse("2024-05-15") },
       new Inventory { InventoryName = "Ketchup", Department= "Dry-Goods", Price = 1.99M, Quantity = "15", ExpirationDate = DateTime.Parse("2025-01-12") },
       new Inventory { InventoryName = "Doritos", Department= "Dry-Goods", Price = 5.99M, Quantity = "27", ExpirationDate = DateTime.Parse("2025-07-04") },
       new Inventory { InventoryName = "Twinkies", Department= "Dry-Goods", Price = 3.99M, Quantity = "5", ExpirationDate = DateTime.Parse("2025-06-15") },
       new Inventory { InventoryName = "Canned Green Beans", Department= "Dry-Goods", Price = 0.99M, Quantity = "70", ExpirationDate = DateTime.Parse("2025-05-15") },
       new Inventory { InventoryName = "Penne Pasta", Department= "Dry-Goods", Price = 1.99M, Quantity = "24", ExpirationDate = DateTime.Parse("2025-06-30") },
       
   };
   
   context.AddRange(Inventories);
   context.SaveChanges();
    

    
    List<EmployeeLog> EmployeeLogs = new List<EmployeeLog>
   {
       
       new EmployeeLog { EmployeeID = 1, InventoryID = 1 },
       new EmployeeLog { EmployeeID = 2, InventoryID = 2 },
       new EmployeeLog { EmployeeID = 3, InventoryID = 3 },
   
       
   };
   
   context.AddRange(EmployeeLogs);
   context.SaveChanges();
    }


    
    
}