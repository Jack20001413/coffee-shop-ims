using System;
using CoffeeShopIMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopIMS.ViewModels;

public class PurchaseRequestViewModel
{
    public string OrderPerson { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string WarehouseAddress { get; set; } = string.Empty;
    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [BindProperty]
    public IList<Ingredient> Ingredients { get; set; } = [];
}
