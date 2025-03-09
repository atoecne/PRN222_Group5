using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOs
{
    public class ServicesDAO : IServicesDAO
    {
        protected readonly Prn222Group5Context _context;

        public ServicesDAO(Prn222Group5Context context)
        {
            _context = context;
        }
        public async Task<bool> AddToCart(int productId, int userId, string size, int quantity)
        {
            // Kiểm tra User có tồn tại không
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                throw new Exception("Người dùng không tồn tại trong hệ thống!");
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null && size == cartItem.Size)
            {
                cartItem.Quantity += quantity; 
            }
            else
            {
                var newCartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    Size = size,
                    CreatedAt = DateTime.Now
                };
                await _context.Carts.AddAsync(newCartItem);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        // Lấy danh sách sản phẩm trong giỏ hàng
        public async Task<List<CartItems>> GetCartItems(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            return await _context.Carts
        .Where(c => c.UserId == userId)
        .Include(c => c.Product) // Include để lấy thông tin sản phẩm
        .Select(c => new CartItems
        {
            CartID = c.CartId,
            Size = c.Size,
            ProductName = c.Product.Name,
            ProductImg = c.Product.ImageUrl,
            UnitPrice = c.Product.Price,
            Quantity = c.Quantity,
            CategoryName = c.Product.Category.CategoryName,
        })
        .ToListAsync();
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await  _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
