using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using BLL.Interface;

namespace UI_MVC.Controllers
{
    public class CartsController : Controller
    {
        private readonly Prn222Group5Context _context;
        private readonly IServicesReps  _serviceRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public CartsController(Prn222Group5Context context, IServicesReps serviceRepository, IRepository<Cart> cartRepository, IRepository<Product> productRepository)
        {
            _context = context;
            _serviceRepository = serviceRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest model)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            int quantity =await _serviceRepository.QuantityInWareHouseByProductId(model.ProductId);

            if(quantity < model.Quantity)
            {
                return Json(new { success = false, message = "Không thể thêm sản phẩm!" });
            }

            bool isAdded = await _serviceRepository.AddToCart(model.ProductId, userId.Value, model.Size, model.Quantity);

            if (isAdded)
            {
                return Json(new { success = true, message = "Sản phẩm đã thêm vào giỏ hàng!" });
            }
            return Json(new { success = false, message = "Không thể thêm sản phẩm!" });
        }
        // GET: Carts
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserID"); 
            var cartItems = await _serviceRepository.GetAllCartByUserId(userId.Value);
            return View(cartItems);
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId,ProductId,Quantity,CreatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", cart.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5

        [HttpGet("Carts/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        { 

            if (id == 0)
            {
                return NotFound();
            }

            var cart = await _cartRepository.GetById(id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId,ProductId,Quantity,CreatedAt,Size")] Cart cart)
        {
            int quantity = await _serviceRepository.QuantityInWareHouseByProductId(cart.ProductId);

            if (quantity < cart.Quantity)
            {
                return Json(new { success = false, message = "Số lượng trong kho không đủ!" });
            }
            await _cartRepository.Update(cart);
            return Json(new { success = true, message = "Thay đổi thành công!" });
        }

        //GET: Carts/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _cartRepository.GetById(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        // POST: Carts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _cartRepository.Delete(id);
        //    return RedirectToAction("Index");
        //}

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cartItem = _context.Carts.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cartItem);
            _context.SaveChanges();
            //_serviceRepository.DeleteCartById(id);
            return Json(new { success = true });
        }
    }
}
