using System;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Repositories.Implementations;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly ApplicationDbContext _context;

    public WarehouseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Warehouse FindById(int id)
    {
        return _context.Warehouses
            .AsNoTracking()
            .Where(w => w.Id == id)
            .First();
    }

    public List<Warehouse> GetAll()
    {
        return _context.Warehouses
            .AsNoTracking()
            .ToList();
    }
}
