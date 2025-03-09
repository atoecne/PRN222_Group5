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
    }
}
