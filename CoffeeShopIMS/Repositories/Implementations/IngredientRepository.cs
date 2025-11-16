using System;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;

namespace CoffeeShopIMS.Repositories.Implementations;

public class IngredientRepository : IIngredientRepository
{
    private readonly ApplicationDbContext _context;

    public IngredientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }

    public List<Ingredient> GetAll()
    {
        return _context.Ingredients.ToList();
    }

    public string? GetIngredientUnit(string ingredient)
    {
        return _context.Ingredients
            .Where(i => string.Equals(i.Name, ingredient))
            .Select(i => i.Unit)
            .FirstOrDefault();
    }
}
