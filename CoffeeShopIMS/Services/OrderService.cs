using System;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Interfaces;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Utils;
using CoffeeShopIMS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private const int PAGE_SIZE = 10;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseOrder> ListOrders(int currentPage = 1, int nextPage = 1, int pageSize = PAGE_SIZE)
    {
        return _context.PurchaseOrders
            .AsNoTracking()
            .OrderBy(o => o.CreationDate)
            .Where(o => o.Id <= (nextPage * pageSize) && o.Id > (currentPage * pageSize))
            .Take(pageSize)
            .ToList();
    }

    public int Create(PurchaseRequestReceiveViewModel data)
    {
        var supplier = _context.Suppliers.SingleOrDefault(s => s.Id == data.SupplierId);

        if (supplier is null)
        {
            return 0;
        }

        var order = new PurchaseOrder
        {
            CreationDate = data.CreationDate,
            OrderPerson = data.OrderPerson!,
            Supplier = supplier,
            OrderNumber = Randomizer.GenerateOrderCode(),
            UpdatedAt = DateTime.UtcNow,
            OrderDetails = data.OrderedIngredients!,
            WarehouseId = data.WarehouseId
        };
        _context.PurchaseOrders.Add(order);

        foreach (var item in data.OrderedIngredients!)
        {
            var ingredient = _context.Ingredients.Find(item.IngredientId);
            if (ingredient is not null)
            {
                ingredient.Quantity += item.Quantity;
                _context.Ingredients.Update(ingredient);
            }
        }

        _context.SaveChanges();

        return 1;
    }

    public void Filter(string oderPerson, DateOnly creationDate, string status)
    {
        
    }
}
