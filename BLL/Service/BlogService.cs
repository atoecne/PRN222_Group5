using BLL.Interface;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BLL.Service
{
    public class BlogService : IBlogService
    {
        private readonly Prn222Group5Context _context;
        private readonly string _imageFolderPath = "wwwroot/images/blogs"; // Đường dẫn tương đối từ thư mục gốc

        public BlogService(Prn222Group5Context context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.User).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.User).FirstOrDefaultAsync(b => b.BlogId == id);
        }

        public async Task<Blog> CreateBlogAsync(Blog blog, IFormFile? BlogImg)
        {
            // Kiểm tra nếu không có hình ảnh
            if (BlogImg == null || BlogImg.Length == 0)
            {
                throw new ArgumentException("Blog image is required", nameof(BlogImg));
            }

            // Xử lý hình ảnh
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(BlogImg.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Chỉ chấp nhận các tệp ảnh có định dạng .jpg, .jpeg, .png, .gif");
            }

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath, fileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath)))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await BlogImg.CopyToAsync(stream);
            }

            blog.BlogImg = $"/images/blogs/{fileName}"; // Đường dẫn tương đối để truy cập từ trình duyệt
            blog.CreatedAt = DateTime.UtcNow;
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> UpdateBlogAsync(Blog blog, IFormFile? BlogImg)
        {
            var existingBlog = await _context.Blogs.FindAsync(blog.BlogId);
            if (existingBlog == null) throw new ArgumentException("Blog not found");

            existingBlog.BlogTitle = blog.BlogTitle;
            existingBlog.BlogContent = blog.BlogContent;
            existingBlog.UserId = blog.UserId;

            if (BlogImg != null && BlogImg.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(BlogImg.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Chỉ chấp nhận các tệp ảnh có định dạng .jpg, .jpeg, .png, .gif");
                }

                if (!string.IsNullOrEmpty(existingBlog.BlogImg))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBlog.BlogImg.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath, fileName);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _imageFolderPath));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await BlogImg.CopyToAsync(stream);
                }

                existingBlog.BlogImg = $"/images/blogs/{fileName}";
            }

            _context.Entry(existingBlog).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lưu vào database: {ex.Message}");
            }

            return existingBlog;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return false;

            if (!string.IsNullOrEmpty(blog.BlogImg))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.BlogImg.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}