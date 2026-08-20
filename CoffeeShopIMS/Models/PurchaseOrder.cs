using System;
using CoffeeShopIMS.Enums;

namespace CoffeeShopIMS.Models;

public class PurchaseOrder
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateOnly CreationDate { get; set; }
    public string OrderPerson { get; set; } = string.Empty;
    public string Status { get; set; } = OrderStatus.Pending.ToString();
    public int SupplierId { get; set; }
    public int WarehouseId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public ICollection<PurchaseOrderDetail> OrderDetails { get; set; } = [];
    public Supplier Supplier { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
}
