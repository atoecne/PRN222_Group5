using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using BLL.Interface;

namespace UI_Razor.Pages.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogService blogService;
        private readonly Prn222Group5Context _context;

        public EditModel(IBlogService blogService, Prn222Group5Context context)
        {
            this.blogService = blogService;
            _context = context;

        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        [BindProperty]
        public IFormFile? BlogImg { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var blog = await blogService.GetBlogByIdAsync(id.Value);
            if (blog == null) return NotFound();

            Blog = blog;
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var updatedBlog = await blogService.UpdateBlogAsync(Blog, BlogImg); // Truyền cả Blog và BlogImg
            if (updatedBlog == null) return NotFound();

            return RedirectToPage("./Index");
        }
    }
}