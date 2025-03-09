using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ImportOrder
{
    public int ImportOrderId { get; set; }

    public string Supplier { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime? ImportDate { get; set; }

    public virtual ICollection<ImportOrderDetail> ImportOrderDetails { get; set; } = new List<ImportOrderDetail>();

    public virtual User User { get; set; } = null!;
}
