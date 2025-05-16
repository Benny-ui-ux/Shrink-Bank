using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shrink_Bank.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
 

namespace Shrink_Bank.Pages
{
    public class LoginModel : PageModel
    {
        private readonly InventoryContext _context;

        public LoginModel(InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        // The ReturnUrl property is REMOVED
        // public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

    
        public async Task<IActionResult> OnPostAsync() 
        {

            if (ModelState.IsValid)
            {
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Name == Input.Username && e.Password == Input.Password);

                if (employee != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employee.Name ?? "UnknownUser"),
                        new Claim(ClaimTypes.NameIdentifier, employee.EmployeeID.ToString()),
                        new Claim("Department", employee.Department ?? "N/A"),
                        new Claim(ClaimTypes.Role, employee.Position ?? "Stocker")
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return LocalRedirect(Url.Content("~/"));
                   
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // 
            return Page();
        }
    }
}