using System;
using System.Collections.Generic;

namespace FashionShops.Models;

public partial class ProductVoting
{
    public int Id { get; set; }

    public int? Star { get; set; }

    public string? Review { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public int? ProductId { get; set; }

    public virtual Member? CreateByNavigation { get; set; }

    public virtual Product? Product { get; set; }
}
