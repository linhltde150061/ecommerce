using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class NewsCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
