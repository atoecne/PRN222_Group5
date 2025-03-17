using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using BLL.Interface;
using Microsoft.CodeAnalysis;
using System.Drawing.Printing;

namespace UI_MVC.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly IRepository<Product> _productRepo;     
        private readonly IRepository<User> _userRepo;
        private readonly IFeedback _fbService;
        private readonly IRepository<Feedback> _feedbackRepo;

        public FeedbacksController(IRepository<Product> productRepo, IRepository<User> userRepo, IFeedback fbService, IRepository<Feedback> feedbackRepo)
        {      
            _productRepo = productRepo;
            _userRepo = userRepo;
            _feedbackRepo = feedbackRepo;
            _fbService = fbService;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index(int productId, int pageNumber = 1, int ratingFilter = 0)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            ViewBag.CurrentUserId = userId;

            int pageSize = 10;

            var feedbacks = await _fbService.GetFbByProductPaged(productId, pageNumber, pageSize, ratingFilter);
            var fbAllByPr = await _fbService.GetFbByProduct(productId);

            ViewBag.ProductId = productId;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(fbAllByPr
                .Count() / (double)pageSize);
            ViewBag.RatingFilter = ratingFilter;
            return View(feedbacks);
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            ViewBag.CurrentUserId = userId;           
            
            var feedbacksByProduct = await _fbService.GetFbByProduct(id);
            
            ViewBag.Feedbacks = feedbacksByProduct;

            return View(); 
        }

        // GET: Feedbacks/Create
        public async Task<IActionResult> Create(int productId)
        {
            Console.WriteLine("get creare");
            var feedback = new Feedback
            {
                ProductId = productId
            };
            return View(feedback);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ProductId,Rating,Comment,CreatedAt,FeedbackImg")] Feedback feedback, IFormFile? imageFile)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            feedback.UserId = userId.Value;
           
            await _fbService.AddFeedback(feedback, imageFile);
            return RedirectToAction("Details", "Products", new { id = feedback.ProductId });
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var feedback = await _feedbackRepo.GetById(id);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,UserId,ProductId,Rating,Comment")] Feedback feedback, IFormFile? imageFile)
        {
            var UserId = HttpContext.Session.GetInt32("UserID");
            feedback.UserId = UserId.Value;

            if (feedback.Rating < 1 || feedback.Rating > 5)
            {
                ModelState.AddModelError("Rating", "Rating phải nằm trong khoảng từ 1 đến 5.");
                return View(feedback);
            }
            try
            {
                await _fbService.UpdateFeedback(feedback, imageFile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(feedback);
            }

            return RedirectToAction("Index", new { productId = feedback.ProductId });
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var feedback = await _feedbackRepo.GetById(id.Value);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _feedbackRepo.GetById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            await _feedbackRepo.Delete(id);
            return RedirectToAction("Index", new { productId = feedback.ProductId });
        }
    }
}
