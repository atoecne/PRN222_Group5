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
        public List<CartItems> CartItems { get; set; }
    }
}
