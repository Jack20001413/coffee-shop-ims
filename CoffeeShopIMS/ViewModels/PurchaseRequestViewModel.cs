using System;
using System.ComponentModel.DataAnnotations;
using CoffeeShopIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeShopIMS.ViewModels;

public class PurchaseRequestViewModel
{
    public PurchaseRequestLoadViewModel LoadViewModel { get; set; }
    public PurchaseRequestReceiveViewModel? ReceiveViewModel { get; set; }
}

public record struct PurchaseRequestLoadViewModel
{
    public SelectList? Ingredients { get; set; }
    public SelectList? Vendors { get; set; }
    public SelectList? Warehouses { get; set; }
}

public class PurchaseRequestReceiveViewModel
{
    [Required(ErrorMessage = "Order person's name is required.")]
    public string? OrderPerson { get; set; }
    public int SupplierId { get; set; }
    public int WarehouseId { get; set; }
    public DateOnly CreationDate { get; set; }
    public IList<PurchaseOrderDetail>? OrderedIngredients { get; set; }
}