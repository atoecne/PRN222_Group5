using DAL.DTOs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOs
{
    public class ReportDAO : IReportDAO
    {
        private readonly Prn222Group5Context _context;

        public ReportDAO(Prn222Group5Context context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var details = await _context.OrderDetails
             .Include(od => od.Product)
             .Where(od => od.OrderId == orderId)
             .ToListAsync();

            return details;
        }

        public async Task<List<ReportDTO>> GetReport(int? day, int? month, int? year)
        {
            var query = _context.Orders.Include(o => o.User).AsQueryable();
            
            if (day.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Day == day);

            if (month.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Month == month);

            if (year.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Year == year);

            return await query
                .Select(o => new ReportDTO
                {
                    OrderId = o.OrderId,
                    CreatedAt = o.CreatedAt.Value,
                    TotalAmount = o.TotalAmount,
                    Name = o.User.FullName
                })
                .ToListAsync();
        }

        public async Task<ReportDTO> GetReportSummary(int? day, int? month, int? year)
        {
            var query = _context.Orders
        .Include(o => o.OrderDetails)
        .ThenInclude(d => d.Product)
        .AsQueryable();

            if (year.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Year == year.Value);
            if (month.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Month == month.Value);
            if (day.HasValue)
                query = query.Where(o => o.CreatedAt.Value.Day == day.Value);

            var orders = await query.ToListAsync();

            // Tính tổng doanh thu
            var totalRevenue = orders.Sum(o => o.TotalAmount);

            // Đếm số đơn hàng
            var totalOrders = orders.Count();

            // Lấy danh sách tất cả các chi tiết đơn hàng
            var orderDetails = orders.SelectMany(o => o.OrderDetails);

            // Gom nhóm theo sản phẩm để tìm sản phẩm bán chạy
            var topProduct = orderDetails
                .GroupBy(d => d.Product.Name)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(d => d.Quantity)
                })
                .OrderByDescending(g => g.TotalSold)
                .FirstOrDefault();

            return new ReportDTO
            {
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                TopProductName = topProduct?.ProductName ?? "Không có dữ liệu",
                TopProductSold = topProduct?.TotalSold ?? 0
            };
        }
    }
}

