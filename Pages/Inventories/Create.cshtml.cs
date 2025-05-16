using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shrink_Bank.Models;

namespace Shrink_Bank.Pages.Inventories
{
    public class CreateModel : PageModel
    {
        private readonly Shrink_Bank.Models.InventoryContext _context;

        public CreateModel(Shrink_Bank.Models.InventoryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        [BindProperty]
        public Inventory Inventory { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
    { 
        return Page();
    }

            _context.Inventories.Add(Inventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
