using BLL.Interface;
using DAL.DAOs;
using DAL.DTOs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ReportService : IReport
    {
        private readonly IReportDAO _report;

        public ReportService(IReportDAO report)
        {
            _report = report;
        }

        public async Task<List<ReportDTO>> GetReport(int? day, int? month, int? year)
        {
            var reports = await _report.GetReport(day, month, year);
            return reports;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int OrderId)
        {
            return await _report.GetOrderDetails(OrderId);
        }

        public async Task<ReportDTO> GetReportSummary(int? day, int? month, int? year)
        {
            return await _report.GetReportSummary(day, month, year);
        }
    }
}