using BLL.Interface;
using DAL.DAOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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


        public Task<bool> AddToCart(int productId, int userId, string size, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(int id)
        {
            return _servicesDAO.DeleteUser(id);
        }

        public Task<List<CartItems>> GetCartItems(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            return _servicesDAO.GetUserById(id);
        }

        public Task<List<User>> GetUsers()
        {
            // Ví dụ: trả về danh sách user (bạn có thể gọi GetUsersAsync)
            return _servicesDAO.GetUsersAsync();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _servicesDAO.GetUsersAsync();
        }

        public Task<bool> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(User user)
        {
            return _servicesDAO.UpdateUser(user);
        }

       
    }
}
