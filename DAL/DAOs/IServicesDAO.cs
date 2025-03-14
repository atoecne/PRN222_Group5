using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOs
{
    public interface IServicesDAO
    {
        Task<bool> AddToCart(int productId ,int userId, string size, int quantity);
        Task<List<CartItems>> GetCartItems(int userId);
        Task<User> GetUser(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task<int> SaveChangesAsync();
        // Thêm phương thức này
        Task AddUser(User user);
        Task<bool> RegisterUser(User user);
 
      
     
    }
}
