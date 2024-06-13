using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class Member
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    public virtual ICollection<ProductVoting> ProductVotings { get; set; } = new List<ProductVoting>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
