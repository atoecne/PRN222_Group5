using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public string BlogImg { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
