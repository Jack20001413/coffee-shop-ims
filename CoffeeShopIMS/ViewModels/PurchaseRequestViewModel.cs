using System;
using CoffeeShopIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeShopIMS.ViewModels;

public class PurchaseRequestViewModel
{
    public string OrderPerson { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public SelectList? Vendors { get; set; }
    public string WarehouseAddress { get; set; } = string.Empty;
    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [BindProperty]
    public IList<Ingredient> OrderedIngredients { get; set; } = [];
    public SelectList? Ingredients { get; set; }
}
