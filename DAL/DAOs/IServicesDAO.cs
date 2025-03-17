using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DAOs
{
    public interface IServicesDAO
    {
        Task<bool> AddToCart(int productId, int userId, string size, int quantity);
        Task<List<CartItems>> GetCartItems(int userId);
        Task<User> GetUser(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmailOrPhone(string email, string phone);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<int> SaveChangesAsync();
        Task<User> GetUserByEmail(string email);
    }
}
