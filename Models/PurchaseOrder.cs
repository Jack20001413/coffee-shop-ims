using System;

namespace coffee_shop_ims.Models;

public class PurchaseOrder
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public DateOnly CreationDate { get; set; }

    public string OrderPerson { get; set; } = string.Empty;

    public int SupplierId { get; set; }

    public int WarehouseId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public ICollection<PurchaseOrderDetail> OrderDetails { get; } = new List<PurchaseOrderDetail>();
    public Supplier Supplier { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
}
