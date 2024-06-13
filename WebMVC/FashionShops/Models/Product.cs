using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Picture { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? Information { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual ProductBrand? Brand { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public virtual Member? CreateByNavigation { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

    public virtual ICollection<ProductVoting> ProductVotings { get; set; } = new List<ProductVoting>();
}
