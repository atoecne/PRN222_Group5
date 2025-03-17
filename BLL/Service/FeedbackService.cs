using BLL.Interface;
using DAL.DAOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace BLL.Service
{
    public class FeedbackService : IFeedback
    {
        private readonly IServicesDAO _servicesDAO;
        private readonly IGenericDAO<Feedback> _feedbackDAO;
        private readonly string _imageFolderPath = "wwwroot/images/feedback";
        public FeedbackService(IServicesDAO services, IGenericDAO<Feedback> feedbackDAO) 
        {   
            _servicesDAO = services;
            _feedbackDAO = feedbackDAO;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _servicesDAO.GetAllFeedbacks();
        }
        public async Task<bool> UpdateFeedback(Feedback feedback, IFormFile? imageFile)
        {
            var crFeedback = await _feedbackDAO.GetById(feedback.FeedbackId);

            crFeedback.Rating = feedback.Rating;
            crFeedback.Comment = feedback.Comment;
            crFeedback.UserId = feedback.UserId;
            crFeedback.CreatedAt = DateTime.Now;
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_imageFolderPath);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(crFeedback.FeedbackImg))
                {
                    var oldFilePath = Path.Combine("wwwroot", crFeedback.FeedbackImg.TrimStart('/'));

                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                // Lưu ảnh mới
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn ảnh mới cho feedback
                crFeedback.FeedbackImg = "/images/feedback/" + fileName;
            }
            await _feedbackDAO.Update(crFeedback);
            return true;
        }
        public async Task<List<Feedback>> GetFbByProduct(int id)
        {
            var allFeedbacks = await _servicesDAO.GetAllFeedbacks();
            var fbByProduct = allFeedbacks
                          .Where(f => f.ProductId == id)
                          .OrderByDescending(f => f.CreatedAt)
                          .ToList();
            return fbByProduct;
        }
        public async Task<List<Feedback>> GetFbByProductPaged(int productId, int pageNumber, int pageSize, int ratingFilter = 0)
        {
            var fbByProduct = await _servicesDAO.GetFbByProductPaged(productId, pageNumber, pageSize, ratingFilter);

            return fbByProduct;
        }

        public async Task AddFeedback(Feedback feedback, IFormFile? imageFile)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_imageFolderPath);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Gán đường dẫn ảnh cho Feedback
                feedback.FeedbackImg = "/images/feedback/" + fileName;
            }

            // Lưu feedback vào DB
            await _feedbackDAO.Add(feedback);
        }
    }
}
