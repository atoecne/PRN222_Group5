using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DAL.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
    public string Size { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber {  get; set; }
    public string Address { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
