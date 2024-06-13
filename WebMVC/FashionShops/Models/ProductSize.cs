using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductSize
{
    public int Id { get; set; }

    public string? Size { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
}
