namespace Shrink_Bank.Models
{
    public class Employee
    {
        
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Department { get; set; }
        public string? Position {get; set; }

        public List<EmployeeLog> EmployeeLogs {get; set;} = default!;
   
    }
}