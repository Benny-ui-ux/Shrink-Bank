using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore;
using Shrink_Bank.Models;

namespace Shrink_Bank.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly Shrink_Bank.Models.InventoryContext _context;

        public RegisterModel(Shrink_Bank.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = default!;

        public List<SelectListItem> DepartmentOptions { get; set; } = new List<SelectListItem>();

        private void PopulateDepartmentOptions()
        {
            DepartmentOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Frozen", Text = "Frozen Foods" },
                new SelectListItem { Value = "Produce", Text = "Produce Section" },
                new SelectListItem { Value = "Dairy", Text = "Dairy & Refrigerated" },
                new SelectListItem { Value = "Dry-Goods", Text = "Dry Goods & Aisles" },
            };
        }

        
        public void OnGet()
        {
            PopulateDepartmentOptions();
        }

        
        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = await _context.Employees
                                                 .FirstOrDefaultAsync(e => e.Name == Input.Username);
                if (existingEmployee != null)
                {
                    ModelState.AddModelError(string.Empty, "Username already taken. Please choose a different name.");
                    PopulateDepartmentOptions(); 
                    return Page();
                }

                var employee = new Employee
                {
                    Name = Input.Username,
                    Password = Input.Password, 
                    Department = Input.Department,
                    Position = "Stocker"
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "Registration successful! Please log in.";
                return RedirectToPage("./Login"); 
            }

            
            PopulateDepartmentOptions(); 
            return Page();
        }
    }
}