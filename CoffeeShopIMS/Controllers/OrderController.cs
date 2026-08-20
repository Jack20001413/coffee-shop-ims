using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Utils;
using CoffeeShopIMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PAGE_SIZE = 10;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int currentPage = 0)
    {
        int nextPage = currentPage + 1;

        var orders = _context.PurchaseOrders
            .AsNoTracking()
            .OrderBy(o => o.CreationDate)
            .Where(o => o.Id <= (nextPage * PAGE_SIZE) && o.Id > (currentPage * PAGE_SIZE))
            .Take(PAGE_SIZE)
            .ToList();

        var model = new OrderHistoryViewModel
        {
          Orders = orders,
          OrderCount = _context.PurchaseOrders.Count()
        };

        return View(model);
    }

    public IActionResult Create()
    {
        var model = new PurchaseRequestViewModel(_context);
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(PurchaseRequestViewModel data)
    {
        if ((data is null) || (data.ReceiveViewModel is null))
        {
            return BadRequest("No data received from client.");
        }

        var receivedData = data.ReceiveViewModel;

        if (!ModelState.IsValid)
        {
            data.LoadViewModel = new PurchaseRequestLoadViewModel().LoadData(_context);
        }

        var supplier = _context.Suppliers.SingleOrDefault(s => s.Id == receivedData.SupplierId);

        if (supplier is null)
        {
            return NotFound("Supplier not found.");
        }

        var order = new PurchaseOrder
        {
            CreationDate = receivedData.CreationDate,
            OrderPerson = receivedData.OrderPerson!,
            Supplier = supplier,
            OrderNumber = Randomizer.GenerateOrderCode(),
            UpdatedAt = DateTime.UtcNow,
            OrderDetails = receivedData.OrderedIngredients!,
            WarehouseId = receivedData.WarehouseId
        };
        _context.PurchaseOrders.Add(order);

        foreach (var item in receivedData.OrderedIngredients!)
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

    public IActionResult Filter(string orderPerson, DateOnly creationDate, string status, int currentPage)
    {
        int nextPage = currentPage++;
        var orders = _context.PurchaseOrders
            .AsNoTracking()
            .Where(o => o.OrderPerson == orderPerson)
            .Where(o => o.CreationDate == creationDate)
            .Where(o => o.Status == status)
            .OrderBy(o => o.CreationDate)
            .Where(o => o.Id <= (nextPage * PAGE_SIZE) && o.Id > (currentPage * PAGE_SIZE))
            .Take(PAGE_SIZE)
            .ToList();

        return View(nameof(GetOrderHistoryTablePartial), orders);
    }

    public IActionResult GetOrderHistoryTablePartial(OrderHistoryViewModel orderHistory)
    {
        return PartialView("_OrderHistoryTable", orderHistory);
    }
}
