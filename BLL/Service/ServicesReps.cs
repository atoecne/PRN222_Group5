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

        public Task AddRange(IEnumerable<OrderDetail> orderDetails)
        {
            return _servicesDAO.AddRange(orderDetails);
        }

        public async Task<bool> AddToCart(int productId, int userId, string size, int quantity)
        {
            return await _servicesDAO.AddToCart(productId, userId, size, quantity);
        }

        public async Task<Order> CreateNewOrder(int userId, Decimal totalAmount)
        {
          return await _servicesDAO.CreateNewOrder(userId, totalAmount);
        }

        public async Task DeleteCartById(int cartId)
        {
           await  _servicesDAO.DeleteCartById(cartId);
        }

        public async Task DeleteCartByUserId(int userId)
        {
          await  _servicesDAO.DeleteCartByUserId(userId);
        }

        public Task<List<CartItems>> GetAllCartByUserId(int userId)
        {
            return _servicesDAO.GetAllCartByUserId(userId);
        }

        public Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return _servicesDAO.GetOrderDetailByOrderId(orderId);
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _servicesDAO.GetUser(email, password);
        }

        public async Task<int> ProductIdByName(string name)
        {
            return await _servicesDAO.ProductIdByName(name);
        }

        public Task<int> QuantityInWareHouseByProductId(int productId)
        {
            return _servicesDAO.QuantityInWareHouseByProductId(productId);
        }

        public async Task UpdateQuantity(int productId, int quantity)
        {
            await _servicesDAO.UpdateQuantity(productId, quantity);
        }
    }
}
