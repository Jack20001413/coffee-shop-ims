using System;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public interface ISupplierRepository
{
    public List<Supplier> GetAll();
    public void Create(Supplier supplier);
    public Supplier? GetById(int id);
    public void Update(Supplier supplier);
    public void Delete(Supplier supplier);
    public void CommitChanges();
    public Supplier FindByName(string name);
}
