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
        Task<List<CartItems>> GetCartItems(int userId);
        Task<User> GetUser(string email, string password);
    }
}
