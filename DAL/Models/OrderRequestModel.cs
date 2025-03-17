using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderRequestModel
    {
        public CheckoutViewModel CheckoutData { get; set; } = new CheckoutViewModel();
        public CustomerInfo CustomerData { get; set; } = new CustomerInfo();
    }
}
