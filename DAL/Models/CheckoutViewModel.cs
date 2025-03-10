using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CheckoutViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        // Thông tin khách hàng
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Danh sách sản phẩm trong giỏ hàng
        public List<CartItems> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
