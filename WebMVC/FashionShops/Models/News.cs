using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class News
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Picture { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public int? NewsCategoryId { get; set; }

    public virtual Member? CreateByNavigation { get; set; }

    public virtual NewsCategory? NewsCategory { get; set; }

    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();
}
