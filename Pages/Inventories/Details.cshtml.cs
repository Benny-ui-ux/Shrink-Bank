using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shrink_Bank.Models;

namespace Shrink_Bank.Pages.Inventories
{
    public class DetailsModel : PageModel
    {
        private readonly Shrink_Bank.Models.InventoryContext _context;

        public DetailsModel(Shrink_Bank.Models.InventoryContext context)
        {
            _context = context;
        }

        public Inventory Inventory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FirstOrDefaultAsync(m => m.InventoryID == id);

            if (inventory is not null)
            {
                Inventory = inventory;

                return Page();
            }

            return NotFound();
        }
    }
}
