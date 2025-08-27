using System;

namespace CoffeeShopIMS.Models;

public class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public ICollection<PurchaseOrder> Orders { get; } = new List<PurchaseOrder>();
}
