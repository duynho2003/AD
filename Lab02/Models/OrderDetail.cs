using System;
using System.Collections.Generic;

namespace Lab02.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public string? Photo { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
