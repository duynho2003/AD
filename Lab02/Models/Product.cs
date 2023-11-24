using System;
using System.Collections.Generic;

namespace Lab02.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Photo { get; set; }
    public string? QrCode { get; set; }
    public string? BarCode { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
