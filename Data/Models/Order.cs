using System;
using System.Collections.Generic;

namespace ShopSphere.Data.Models;

public partial class Order
{
    public long OrderId { get; set; }

    public long UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
