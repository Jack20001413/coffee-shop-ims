using System;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public interface ISupplierRepository
{
    public List<Supplier> GetAll();
    public void Create(Supplier supplier);
    public Supplier? GetById(int id);
    public void Update(Supplier supplier);
}
