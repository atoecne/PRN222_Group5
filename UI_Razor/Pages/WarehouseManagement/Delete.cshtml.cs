using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace UI_Razor.Pages.WarehouseManagement
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.Models.Prn222Group5Context _context;

        public DeleteModel(DAL.Models.Prn222Group5Context context)
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

            var warehouse = await _context.WareHouses.FirstOrDefaultAsync(m => m.WareHouseId == id);

            if (warehouse == null)
            {
                return NotFound();
            }
            else
            {
                WareHouse = warehouse;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.WareHouses.FindAsync(id);
            if (warehouse != null)
            {
                WareHouse = warehouse;
                _context.WareHouses.Remove(WareHouse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
