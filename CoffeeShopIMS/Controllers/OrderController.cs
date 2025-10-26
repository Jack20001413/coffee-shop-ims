using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Utils;
using CoffeeShopIMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var orders = _context.PurchaseOrders.AsNoTracking().ToList();
        return View(orders);
    }

    public IActionResult Create()
    {
        var model = new PurchaseRequestViewModel
        {
            LoadViewModel = new PurchaseRequestLoadViewModel
            {
                Ingredients = new SelectList(_context.Ingredients.AsNoTracking().ToList(), nameof(Ingredient.Id), nameof(Ingredient.Name)),
                Vendors = new SelectList(_context.Suppliers.AsNoTracking().ToList(), nameof(Supplier.Name), nameof(Supplier.Name)),
                Warehouses = new SelectList(_context.Warehouses.AsNoTracking().ToList(), nameof(Warehouse.Id), nameof(Warehouse.Address))
            }
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(PurchaseRequestViewModel data)
    {
        var receivedData = data.ReceiveViewModel!;
        var supplier = _context.Suppliers.FirstOrDefault(s => s.Name == receivedData.VendorName);

        if (supplier is null)
        {
            return NotFound($"Supplier {receivedData.VendorName} not found in database.");
        }

        var order = new PurchaseOrder
        {
            CreationDate = receivedData.CreationDate,
            OrderPerson = receivedData.OrderPerson,
            Supplier = supplier,
            OrderNumber = Randomizer.GenerateOrderCode(),
            UpdatedAt = DateTime.UtcNow,
            OrderDetails = receivedData.OrderedIngredients,
            WarehouseId = receivedData.WarehouseId
        };
        _context.PurchaseOrders.Add(order);

        foreach (var item in receivedData.OrderedIngredients)
        {
            var ingredient = _context.Ingredients.Find(item.IngredientId);
            if (ingredient is not null)
            {
                ingredient.Quantity += item.Quantity;
                _context.Ingredients.Update(ingredient);
            }
        }

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
