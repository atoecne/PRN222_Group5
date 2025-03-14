using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class User
{
    public int UserId { get; set; }
    [Required(ErrorMessage = "Họ và tên không được để trống!")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Email không được để trống!")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống!")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "số điện thoại không được để trống!")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ!")]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "địa chỉ không được để trống!")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Ngày sinh không được để trống!")]
    public DateTime Birthday { get; set; }


    public string Role { get; set; } = "Customer";
    
    public string? Destiny { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ImportOrder> ImportOrders { get; set; } = new List<ImportOrder>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
