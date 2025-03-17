using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BLL.Interface
{
    public interface IFeedback
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<bool> UpdateFeedback(Feedback feedback, IFormFile? imageFile);
        Task<List<Feedback>> GetFbByProduct(int id);     
        Task AddFeedback(Feedback feedback, IFormFile? imageFile);
        Task<List<Feedback>> GetFbByProductPaged(int productId, int pageNumber, int pageSize, int ratingFilter = 0);
    }
}
