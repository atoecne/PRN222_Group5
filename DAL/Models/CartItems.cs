using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CartItems
    {
        public int CartID { get; set; }

        public string Size { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public string CategoryName { get; set; } // Lấy từ bảng Category
    }
}
