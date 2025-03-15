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
    public class IndexModel : PageModel
    {
        private readonly IBlogService _blogService;

        public IndexModel(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IList<Blog> Blog { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Blog = await _blogService.GetAllBlogsAsync();
        }
    }
}