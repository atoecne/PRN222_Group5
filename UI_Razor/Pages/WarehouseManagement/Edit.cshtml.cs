using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace UI_Razor.Pages.WarehouseManagement
{
    public class EditModel : PageModel
    {
        private readonly DAL.Models.Prn222Group5Context _context;

        public EditModel(DAL.Models.Prn222Group5Context context)
        {
            _context = context;
        }

        [BindProperty]
        public WareHouse WareHouse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse =  await _context.WareHouses.FirstOrDefaultAsync(m => m.WareHouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            WareHouse = warehouse;
           ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WareHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WareHouseExists(WareHouse.WareHouseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WareHouseExists(int id)
        {
            return _context.WareHouses.Any(e => e.WareHouseId == id);
        }
    }
}
