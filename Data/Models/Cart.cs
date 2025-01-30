using System;
using System.Collections.Generic;

namespace ShopSphere.Data.Models;

public partial class Cart
{
    public long CartId { get; set; }

    public long UserId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
