using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductBrand
{
    public int Id { get; set; }

    public string? BrandName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
