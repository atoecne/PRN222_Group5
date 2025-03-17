//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using BLL.Interface;
//using DAL.Models;

//namespace UI_MVC.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly IServicesReps _service;

//        public UsersController(IServicesReps service)
//        {
//            _service = service;
//        }

//        // Trang đăng nhập
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(string email, string password)
//        {
//            try
//            {
//                var user = await _service.GetUser(email, password);
//                if (user != null)
//                {
//                    HttpContext.Session.SetString("UserRole", user.Role);
//                    HttpContext.Session.SetInt32("UserID", user.UserId);
//                    return RedirectToAction("Index", "Products");
//                }
//                ViewData["Error"] = "Email hoặc mật khẩu không đúng.";
//                return View();
//            }
//            catch (Exception ex)
//            {
//                ViewData["Error"] = "Đã xảy ra lỗi: " + ex.Message;
//                return View();
//            }
//        }

//        // Trang đăng ký
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(User user)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(user);
//            }
//            try
//            {
//                var success = await _service.Register(user);
//                if (!success)
//                {
//                    ViewData["Error"] = "Email hoặc số điện thoại đã tồn tại!";
//                    return View(user);
//                }
//                TempData["Success"] = "Đăng ký thành công!";
//                return RedirectToAction("Login");
//            }
//            catch (Exception ex)
//            {
//                ViewData["Error"] = "Đã xảy ra lỗi: " + ex.Message;
//                return View(user);
//            }
//        }

//        // Danh sách user (Admin)
//        public async Task<IActionResult> Index()
//        {
//            var users = await _service.GetUsers();
//            return View(users);
//        }

//        // Chi tiết user
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var user = await _service.GetUserById(id.Value);
//            if (user == null)
//                return NotFound();

//            return View(user);
//        }

//        // Tạo mới user (nếu cần)
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(User user)
//        {
//            if (!ModelState.IsValid)
//                return View(user);

//            var success = await _service.Register(user);
//            if (!success)
//            {
//                ViewData["Error"] = "Email đã tồn tại!";
//                return View(user);
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        // Chỉnh sửa user (profile)
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var user = await _service.GetUserById(id.Value);
//            if (user == null)
//                return NotFound();

//            return View(user);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, User user)
//        {
//            if (id != user.UserId)
//                return NotFound();

//            if (!ModelState.IsValid)
//                return View(user);

//            var success = await _service.UpdateUser(user);
//            if (!success)
//                return NotFound();

//            return RedirectToAction(nameof(Index));
//        }

//        // Xóa user
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var user = await _service.GetUserById(id.Value);
//            if (user == null)
//                return NotFound();

//            return View(user);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var success = await _service.DeleteUser(id);
//            if (!success)
//                return NotFound();

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}

