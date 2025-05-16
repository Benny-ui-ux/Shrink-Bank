using System.Security.Claims; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shrink_Bank.Models;

namespace Shrink_Bank.Pages.Inventories
{
    [Authorize] 
    public class IndexModel : PageModel
    {
        private readonly Models.InventoryContext _context;

        public IndexModel(Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<InventoryViewModel> InventoryDisplayList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            
            DateTime now = DateTime.Now; 
            DateTime today6AM = now.Date.AddHours(6);
            DateTime cutoffDateTime;

            if (now < today6AM)
            {
                cutoffDateTime = today6AM.AddDays(-1); 
            }
            else
            {
                cutoffDateTime = today6AM; 
            }

            var currentUserDepartment = User.FindFirstValue("Department"); 

            var inventoryItems = await _context.Inventories.ToListAsync();

       
            var relevantLogInventoryIds = inventoryItems.Select(i => i.InventoryID).ToList();
            var allRecentLogs = await _context.EmployeeLogs
                .Include(el => el.Employee)
                .Where(el => relevantLogInventoryIds.Contains(el.InventoryID) && el.LogTimestamp >= cutoffDateTime.AddDays(-2)) // Fetch logs from last ~2 days for efficiency
                .OrderByDescending(el => el.LogTimestamp)
                .ToListAsync();


            InventoryDisplayList = inventoryItems.Select(inv =>
            {
           
                var absoluteLastLog = allRecentLogs
                                        .Where(el => el.InventoryID == inv.InventoryID)
                                        .FirstOrDefault(); 

              
                var lastLogThisPeriod = allRecentLogs
                                        .Where(el => el.InventoryID == inv.InventoryID && el.LogTimestamp >= cutoffDateTime)
                                        .FirstOrDefault(); 

                var daysUntilExpiration = (inv.ExpirationDate.Date - now.Date).Days;
                string expirationStatus;
                string rowClass = "";

                if (daysUntilExpiration < 0)
                {
                    expirationStatus = "Expired";
                    rowClass = "table-danger";
                }
                else if (daysUntilExpiration <= 7)
                {
                    expirationStatus = $"Expires in {daysUntilExpiration} day(s)";
                    rowClass = "table-warning";
                }
                else
                {
                    expirationStatus = "OK";
                }

                bool canCheck = !string.IsNullOrEmpty(currentUserDepartment) &&
                                currentUserDepartment.Equals(inv.Department, StringComparison.OrdinalIgnoreCase);

                return new InventoryViewModel
                {
                    InventoryID = inv.InventoryID,
                    InventoryName = inv.InventoryName ?? string.Empty,
                    Department = inv.Department,
                    Price = inv.Price ?? 0.0M,
                    Quantity = inv.Quantity,
                    ExpirationDate = inv.ExpirationDate,
                    DaysUntilExpiration = daysUntilExpiration,
                    ExpirationStatus = expirationStatus,

                    AbsoluteLastCheckedBy = absoluteLastLog?.Employee?.Name,
                    AbsoluteLastCheckedDate = absoluteLastLog?.LogTimestamp,

                    IsCheckedThisPeriod = lastLogThisPeriod != null,
                    CheckedThisPeriodBy = lastLogThisPeriod?.Employee?.Name,
                    CheckedThisPeriodDate = lastLogThisPeriod?.LogTimestamp,

                    CanCurrentUserCheck = canCheck,
                    RowCssClass = rowClass
                };
            }).ToList();
        }

        public async Task<IActionResult> OnPostCheckItemAsync(int inventoryId)
        {
            var employeeIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserDepartment = User.FindFirstValue("Department");

            if (string.IsNullOrEmpty(employeeIdClaim) || !int.TryParse(employeeIdClaim, out var employeeId) || string.IsNullOrEmpty(currentUserDepartment))
            {
                return Forbid();
            }

            var inventoryItem = await _context.Inventories.FindAsync(inventoryId);
            if (inventoryItem == null)
            {
                return NotFound();
            }

         
            if (!currentUserDepartment.Equals(inventoryItem.Department, StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage();
            }

            DateTime now = DateTime.Now;
            DateTime today6AM = now.Date.AddHours(6);
            DateTime cutoffDateTime = (now < today6AM) ? today6AM.AddDays(-1) : today6AM;



            var logEntry = new EmployeeLog
            {
                EmployeeID = employeeId,
                InventoryID = inventoryId,
                LogTimestamp = DateTime.UtcNow 
            };

            _context.EmployeeLogs.Add(logEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}