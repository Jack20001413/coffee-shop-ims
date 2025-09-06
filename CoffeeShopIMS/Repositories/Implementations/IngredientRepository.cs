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
    
    public List<Ingredient> GetAll()
    {
        return _context.Ingredients.ToList();
    }
}
