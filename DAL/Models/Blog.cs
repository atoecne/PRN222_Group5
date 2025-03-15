using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    [Required(ErrorMessage = "User is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
    public string BlogTitle { get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    public string BlogContent { get; set; } = null!;

    [Required(ErrorMessage = "Image is required")]
    public string? BlogImg { get; set; } = null;

    [Required(ErrorMessage = "Created date is required")]
    public DateTime CreatedAt { get; set; } // Đổi từ nullable sang required vì có default value GETDATE()
    public virtual User User { get; set; } = null!;
}