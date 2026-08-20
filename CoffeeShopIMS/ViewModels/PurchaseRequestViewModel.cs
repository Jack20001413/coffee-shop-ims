using System.ComponentModel.DataAnnotations;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.ViewModels;

public class PurchaseRequestViewModel
{
    public PurchaseRequestLoadViewModel LoadViewModel { get; set; }
    public PurchaseRequestReceiveViewModel? ReceiveViewModel { get; set; }

    public PurchaseRequestViewModel(ApplicationDbContext context)
    {
        LoadViewModel = new PurchaseRequestLoadViewModel().LoadData(context);
    }
}

public record struct PurchaseRequestLoadViewModel
{
    public SelectList? Ingredients { get; set; }
    public SelectList? Vendors { get; set; }
    public SelectList? Warehouses { get; set; }

    public PurchaseRequestLoadViewModel LoadData(ApplicationDbContext context)
    {
        var ingredients = context.Ingredients.AsNoTracking().ToList();

        var suppliers = context.Suppliers.AsNoTracking().ToList();

        var warehouses = context.Warehouses.AsNoTracking().ToList();

        return new PurchaseRequestLoadViewModel
        {
            Ingredients = new SelectList(ingredients, nameof(Ingredient.Id), nameof(Ingredient.Name)),
            Vendors = new SelectList(suppliers, nameof(Supplier.Id), nameof(Supplier.Name)),
            Warehouses = new SelectList(warehouses, nameof(Warehouse.Id), nameof(Warehouse.Address))
        };
    }
}

public class PurchaseRequestReceiveViewModel
{
    [Required(ErrorMessage = "Order person's name is required")]
    public string? OrderPerson { get; set; }

    [Range(1, int.MaxValue - 1, ErrorMessage = "Vendor ID must be greater than 0")]
    public int SupplierId { get; set; }

    [Range(1, int.MaxValue - 1, ErrorMessage = "Warehouse ID must be greater than 0")]
    public int WarehouseId { get; set; }

    public DateOnly CreationDate { get; set; }

    [NonEmptyList(ErrorMessage = "At least one ingredient must be ordered")]
    public IList<PurchaseOrderDetail>? OrderedIngredients { get; set; }
}