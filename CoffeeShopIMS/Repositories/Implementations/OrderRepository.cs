using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }

    public void Create(PurchaseOrder order)
    {
        _context.PurchaseOrders.Add(order);
    }

    public List<PurchaseOrder> GetAll()
    {
        return _context.PurchaseOrders.ToList();
    }
}
