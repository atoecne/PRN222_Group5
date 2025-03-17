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
    public class UserServicesReps : IUserServiceReps
    {
        private readonly IServicesDAO _servicesDAO;

        public UserServicesReps(IServicesDAO servicesDAO)
        {
            _servicesDAO = servicesDAO;
        }

        public async Task<bool> Register(User user)
        {
            // Kiểm tra xem email đã tồn tại chưa (sử dụng GetUserByEmail)
            var existingUser = await _servicesDAO.GetUserByEmail(user.Email);
            if (existingUser != null)
                return false;

            // Mã hóa password bằng MD5
            user.Password = ComputeMD5Hash(user.Password);

            user.Role = "Customer";
            user.CreatedAt = DateTime.Now;
            user.Destiny = GetElement(user.Birthday.Year);

            await _servicesDAO.AddUser(user);
            return await _servicesDAO.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(string email, string password)
        {
            // Mã hóa password trước khi kiểm tra
            string hashedPassword = ComputeMD5Hash(password);
            return await _servicesDAO.GetUser(email, hashedPassword);
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

        public Task<bool> UpdateUser(User user)
        {
            return _servicesDAO.UpdateUser(user);
        }

        /// <summary>
        /// Hàm mã hóa chuỗi input sang MD5.
        /// </summary>
        private string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Tính toán "mệnh" theo năm sinh của user (theo logic cho trước).
        /// </summary>
        private string GetElement(int year)
        {
            string[] heavenlyStems = { "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý" };
            string[] earthlyBranches = { "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi" };
            string[] elements = { "Kim", "Thủy", "Hỏa", "Thổ", "Mộc" };

            int stemIndex = (year - 4) % 10;
            int branchIndex = (year - 4) % 12;


            int total = (stemIndex + branchIndex) % 5; // Luôn đảm bảo chỉ mục hợp lệ

            return elements[total];
        }
    }
}
