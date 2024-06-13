using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class NewsComment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CommentBy { get; set; }

    public int? NewsId { get; set; }

    public virtual Member? CommentByNavigation { get; set; }

    public virtual News? News { get; set; }
}
