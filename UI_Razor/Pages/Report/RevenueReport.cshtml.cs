using BLL.Interface;
using BLL.Service;
using DAL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UI_Razor.Pages.Report
{
    public class RevenueReportModel : PageModel
    {
        private readonly IReport _reportService;
        public RevenueReportModel(IReport reportService)
        {
            _reportService = reportService;
        }
        [BindProperty(SupportsGet = true)]
        public int? Day { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Month { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Year { get; set; }

      
        public async Task<IActionResult> OnGetAsync(int? orderId)
        {           
            var today = DateTime.Now;
            Day ??= today.Day;
            Month ??= today.Month;
            Year ??= today.Year;
            SelectedOrderId = orderId; 
            Reports = await _reportService.GetReport(Day, Month, Year);
            Summary = await _reportService.GetReportSummary(Day, Month, Year);
            if (orderId.HasValue)
            {
                OrderDetails = await _reportService.GetOrderDetails(orderId.Value);
            }

            return Page();
        }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ReportDTO> Reports { get; set; }
        public ReportDTO Summary { get; set; }
        public int? SelectedOrderId { get; set; }
    }
}
