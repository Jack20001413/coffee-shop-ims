using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.ViewModels;

public class OrderHistoryViewModel
{
    public OrderFilterComponent? Filter { get; set; }
    public IEnumerable<PurchaseOrder> Orders { get; set; } = [];
    public int OrderCount { get; set; }
}

public class OrderFilterComponent
{
    public string? OrderPerson { get; set; }
    public DateOnly CreationDate { get; set; }
    public string? Status { get; set; }
}
