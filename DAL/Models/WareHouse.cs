using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class WareHouse
{
    public int WareHouseId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Product Product { get; set; } = null!;
}
