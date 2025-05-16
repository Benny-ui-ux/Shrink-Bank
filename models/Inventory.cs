namespace Shrink_Bank.Models
{
    public class Inventory
    {
        
        public int InventoryID { get; set; }
        public string? InventoryName { get; set; }
        public string?  Department { get; set; }
        public decimal? Price { get; set; }
        public string? Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public List<EmployeeLog> EmployeeLogs { get; set; } = new List<EmployeeLog>();
   
    }

    public class EmployeeLog
{
    public int EmployeeLogID {get; set;} 
    public int EmployeeID {get; set;} 
    public int InventoryID {get; set;} 
    public Employee Employee {get; set;} = default!;
    public Inventory Inventory {get; set;} = default!;
        public DateTime LogTimestamp { get; internal set; }
    }
}