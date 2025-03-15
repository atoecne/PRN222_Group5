using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Models;
using BLL.Interface;

namespace UI_Razor.Pages.Blogs
{
    public class CreateModel : PageModel
    {
        private readonly IBlogService _blogService;
        private readonly Prn222Group5Context _context;

        public CreateModel(IBlogService blogService, Prn222Group5Context context)
        {
            _blogService = blogService;
            _context = context;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        [BindProperty]
        public IFormFile BlogImg { get; set; }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _ = await _blogService.CreateBlogAsync(Blog, BlogImg);
            return RedirectToPage("./Index");
        }
    }
}