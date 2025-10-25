using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Models;

[PrimaryKey(nameof(OrderId), nameof(IngredientId))]
public class PurchaseOrderDetail
{
    public int OrderId { get; set; }
    public int IngredientId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    public PurchaseOrder Order { get; set; } = null!;
    public Ingredient Ingredient { get; set; } = null!;
}
