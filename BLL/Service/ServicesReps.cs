using BLL.Interface;
using DAL.DAOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ServicesReps : IServicesReps
    {
        private readonly IServicesDAO _servicesDAO;

        public ServicesReps(IServicesDAO servicesDAO)
        {
            _servicesDAO = servicesDAO;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _servicesDAO.GetUsersAsync();
        }
        public async Task<bool> AddToCart(int productId, int userId, string size, int quantity)
        {
            return await _servicesDAO.AddToCart(productId, userId, size, quantity);
        }

        public async Task<List<CartItems>> GetCartItems(int userId)
        {
            return await _servicesDAO.GetCartItems(userId);
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _servicesDAO.GetUser(email, password);
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Register(User user)
        {
            // Sửa lỗi: gọi GetUser đúng cách
            var existingUser = await _servicesDAO.GetUser(user.Email, user.Password);
            if (existingUser != null) return false;

            user.Role = "Customer";
            user.CreatedAt = DateTime.Now;

            // Sửa lỗi: dùng AddUser thay vì GetUser.Add
            await _servicesDAO.AddUser(user);

            return await _servicesDAO.SaveChangesAsync() > 0;
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
