using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductColor
{
    public int Id { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
}
