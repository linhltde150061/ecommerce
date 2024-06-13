﻿using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
