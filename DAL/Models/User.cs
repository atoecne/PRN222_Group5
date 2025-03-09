using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly Birthday { get; set; }

    public string Role { get; set; } = null!;

    public string? Destiny { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ImportOrder> ImportOrders { get; set; } = new List<ImportOrder>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
