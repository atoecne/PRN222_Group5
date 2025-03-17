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

        public async Task<Order> CreateNewOrder(int userId, Decimal totalAmout)
        {
            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmout,
                CreatedAt = DateTime.Now
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<CartItems>> GetAllCartByUserId(int userId)
        {
            var cartItems = await (from c in _context.Carts
                                   join p in _context.Products on c.ProductId equals p.ProductId
                                   where c.UserId == userId
                                   select new CartItems
                                   {
                                       CartId = c.CartId,
                                       ProductId = c.ProductId,
                                       Size = c.Size,
                                       ProductName = p.Name,
                                       ProductImg = p.ImageUrl,
                                       UnitPrice = p.Price,
                                       Quantity = c.Quantity
                                   }).ToListAsync();

            return cartItems;
        }
        public async Task<User> GetUser(string email, string password)
        {
            return await  _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<int> ProductIdByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product.ProductId;
        }

        public async Task AddRange(IEnumerable<OrderDetail> orderDetails)
        {
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartByUserId(int userId)
        {
            var useCartItems = _context.Carts.Where(c => c.UserId == userId).ToList();
             _context.Carts.RemoveRange(useCartItems);
            await _context.SaveChangesAsync();
        }

        public async  Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await  _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<int> QuantityInWareHouseByProductId(int productId)
        {
            var wareHouse = await  _context.WareHouses.FirstOrDefaultAsync(w => w.ProductId == productId);
            return wareHouse?.Quantity ?? 0;
        }

        public async Task UpdateQuantity(int productId, int quantity)
        {
            var wareHouse = await _context.WareHouses.FirstOrDefaultAsync(w => w.ProductId == productId);
            wareHouse.Quantity -= quantity;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartById(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null) {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
