using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string Size { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
