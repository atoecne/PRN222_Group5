using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.DAOs
{
    public interface IReportDAO
    {
        Task<List<ReportDTO>> GetReport(int? day, int? month, int? year);
        Task<List<OrderDetail>> GetOrderDetails(int orderId);
        Task<ReportDTO> GetReportSummary(int? day, int? month, int? year);
    }
}

