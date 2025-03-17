using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Models;
using BLL.Interface;

namespace UI_Razor.Pages.WarehouseManagement
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<WareHouse> _warehouseRepo;
        private readonly IRepository<Product> _productRepo;

        public CreateModel(IRepository<WareHouse> warehouseRepo, IRepository<Product> productRepo)
        {
            _warehouseRepo = warehouseRepo;
            _productRepo = productRepo;
        }

        public async Task <IActionResult> OnGet()
        {
        ViewData["ProductId"] = new SelectList(await _productRepo.GetAll(), "ProductId", "Name");
            return Page();
        }

        [BindProperty]
        public WareHouse WareHouse { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _warehouseRepo.Add(WareHouse);

            return RedirectToPage("./Index");
        }
    }
}
