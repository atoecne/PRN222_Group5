using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
