using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IReport
    {
        Task<List<ReportDTO>> GetReport(int? day, int? month, int? year);
        Task<List<OrderDetail>> GetOrderDetails(int OrderId);
        Task<ReportDTO> GetReportSummary(int? day, int? month, int? year);
    }
}
