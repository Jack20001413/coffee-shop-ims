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
}
