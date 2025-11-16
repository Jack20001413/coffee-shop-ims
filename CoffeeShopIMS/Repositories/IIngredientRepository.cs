using System;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories;

public interface IIngredientRepository
{
    public List<Ingredient> GetAll();
    public string? GetIngredientUnit(string ingredient);
    public void CommitChanges();
}
