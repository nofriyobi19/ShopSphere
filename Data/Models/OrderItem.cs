using System;
using System.Collections.Generic;

namespace ShopSphere.Data.Models;

public partial class OrderItem
{
    public long OrderItemId { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
