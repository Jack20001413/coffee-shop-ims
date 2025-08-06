using System;

namespace coffee_shop_ims.Models;

public class Warehouse
{
    public int Id { get; set; }

    public string Address { get; set; } = string.Empty;

    public string PersonInCharge { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public ICollection<PurchaseOrder> Orders { get; } = new List<PurchaseOrder>();
}
