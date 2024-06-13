using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductDetail
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? ColorId { get; set; }

    public int? SizeId { get; set; }

    public int? Amount { get; set; }

    public string? Picture { get; set; }

    public virtual ProductColor? Color { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductSize? Size { get; set; }
}
