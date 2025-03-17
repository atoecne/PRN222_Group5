using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class ReportDTO
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }

        public string Name { get; set; }

        public decimal TotalRevenue { get; set; }       
        public int TotalOrders { get; set; }           
        public string TopProductName { get; set; }      
        public int TopProductSold { get; set; }
    }
}
