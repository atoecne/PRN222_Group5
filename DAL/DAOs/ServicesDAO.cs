using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return true;
        }

        public async Task<bool> AddToCart(int productId, int userId, string size, int quantity)
        {
            // Kiểm tra người dùng có tồn tại không
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
                throw new Exception("Người dùng không tồn tại trong hệ thống!");

            // Nếu sản phẩm đã có trong giỏ hàng với cùng kích cỡ thì tăng số lượng
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

        public async Task<List<CartItems>> GetCartItems(int userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
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
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByEmailOrPhone(string email, string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
