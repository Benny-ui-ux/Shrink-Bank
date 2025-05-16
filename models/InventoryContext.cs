using Microsoft.EntityFrameworkCore;

namespace Shrink_Bank.Models;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
}

    public DbSet<Employee> Employees {get; set;}
    public DbSet<Inventory> Inventories {get; set;}
    public DbSet<EmployeeLog> EmployeeLogs {get; set;}
}