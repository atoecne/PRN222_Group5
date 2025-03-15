using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using BLL.Interface;

namespace UI_Razor.Pages.Blogs
{
    public class DeleteModel : PageModel
    {
        private readonly IBlogService _blogService;

        public DeleteModel(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var blog = await _blogService.GetBlogByIdAsync(id.Value);
            if (blog == null) return NotFound();

            Blog = blog;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var result = await _blogService.DeleteBlogAsync(id.Value);
            if (!result) return NotFound();

            return RedirectToPage("./Index");
        }
    }
}