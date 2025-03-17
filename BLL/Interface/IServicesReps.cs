using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IServicesReps
    {
        Task<bool> AddToCart(int productId, int userId, string size, int quantity);
        Task<User> GetUser(string email, string password);
        Task<Order> CreateNewOrder(int userId, Decimal totalAmount);

        Task<List<CartItems>> GetAllCartByUserId(int userId);
        Task<int> ProductIdByName(string name);

        Task AddRange(IEnumerable<OrderDetail> orderDetails);
        Task DeleteCartByUserId(int userId);

        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);

        Task<int> QuantityInWareHouseByProductId(int productId);

        Task UpdateQuantity(int productId, int quantity);

        Task DeleteCartById(int cartId);
    }
}
