using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Interface
{
    public interface IUserServiceReps
    {
        Task<bool> AddToCart(int productId, int userId, string size, int quantity);
        Task<List<CartItems>> GetCartItems(int userId);
        Task<User> GetUser(string email, string password);
        Task<bool> Register(User user);
        Task<List<User>> GetUsersAsync();
        Task AddUser(User user);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        
    }
}
