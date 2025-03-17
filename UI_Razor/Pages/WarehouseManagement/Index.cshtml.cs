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
    public class IndexModel : PageModel
    {
        private readonly DAL.Models.Prn222Group5Context _context;

        public IndexModel(DAL.Models.Prn222Group5Context context)
        {
            _context = context;
        }

        public IList<WareHouse> WareHouse { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WareHouse = await _context.WareHouses
                .Include(w => w.Product).ToListAsync();
        }
    }
}
