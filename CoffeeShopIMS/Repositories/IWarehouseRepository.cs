using System;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public interface IWarehouseRepository
{
    public Warehouse FindById(int id);
    public List<Warehouse> GetAll();
}
