using System;

namespace coffee_shop_ims.Models;

public class PurchaseOrderDetail
{
    public int OrderId { get; set; }

    public int IngredientId { get; set; }

    public int Quantity { get; set; }

    public string Sku { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public PurchaseOrder Order { get; set; } = null!;
    public Ingredient Ingredient { get; set; } = null!;
}
