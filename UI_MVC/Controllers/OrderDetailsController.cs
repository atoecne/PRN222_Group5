using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using BLL.Interface;
using System.Reflection.Metadata.Ecma335;

namespace UI_MVC.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly Prn222Group5Context _context;
        private readonly IRepository<OrderDetail> _orderdetailsRepository;
        private readonly IServicesReps _service;

        public OrderDetailsController(Prn222Group5Context context, IRepository<OrderDetail> orderdetailRepository, IServicesReps service)
        {
            _context = context;
            _orderdetailsRepository = orderdetailRepository;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrderDetail([FromBody] OrderRequestModel request)
        {
                int? userId = HttpContext.Session.GetInt32("UserID");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                if (request == null || request.CheckoutData == null || request.CustomerData == null)
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ" }); // ✅ Trả về JSON hợp lệ
                }
                var orderDetails = request.CheckoutData.CartItems.Select(item => new OrderDetail
                {
                    OrderId = request.CheckoutData.OrderId,
                    ProductId = item.ProductId,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Price = item.UnitPrice,
                    UserName = request.CustomerData.UserName,
                    PhoneNumber = request.CustomerData.PhoneNumber,
                    Address = request.CustomerData.Address
                }).ToList();
              
                await _service.AddRange(orderDetails);

                foreach (var item in orderDetails)
                {
                 await _service.UpdateQuantity(item.ProductId, item.Quantity);
                }
                await _service.DeleteCartByUserId(userId.Value);

                return Ok(new { message = "Đơn hàng đã được lưu thành công!" });
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index(int? orderId)
        {
            if(orderId == null || orderId == 0)
            {
                return View(new List<OrderDetail>());
            }
            var orderDetails = await _service.GetOrderDetailByOrderId(orderId.Value);
            return View(orderDetails);
        }
        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,ProductId,Quantity,Price,Size,UserName,PhoneNumber,Address")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,ProductId,Quantity,Price,Size,UserName,PhoneNumber,Address")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }
    }
}
