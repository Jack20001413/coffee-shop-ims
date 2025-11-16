using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public interface IOrderRepository
{
    public List<PurchaseOrder> GetAll();
    public void Create(PurchaseOrder order);
    public void CommitChanges();
}
