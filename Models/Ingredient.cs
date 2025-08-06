namespace coffee_shop_ims.Models;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public float Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public ICollection<PurchaseOrderDetail> OrderDetails { get; } = new List<PurchaseOrderDetail>();
}