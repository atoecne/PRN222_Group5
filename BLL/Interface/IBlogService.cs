
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BLL.Interface
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task<Blog> CreateBlogAsync(Blog blog, IFormFile BlogImg);
        Task<Blog?> UpdateBlogAsync(Blog blog, IFormFile? BlogImg);
        Task<bool> DeleteBlogAsync(int id);
    }
}
