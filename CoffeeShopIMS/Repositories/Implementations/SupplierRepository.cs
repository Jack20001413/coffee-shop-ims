using System;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _context;

    public SupplierRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Supplier> GetAll()
    {
        return _context.Suppliers.ToList();
    }

    public void Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    public Supplier? GetById(int id)
    {
        return _context.Suppliers.Find(id);
    }

    public void Update(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        _context.SaveChanges();
    }

    public void Delete(Supplier supplier)
    {
        _context.Suppliers.Remove(supplier);
        _context.SaveChanges();
    }
}
