using CoffeeShopIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Controllers
{
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var ingredients = _context.Ingredients.AsNoTracking().ToList();
            return View(ingredients);
        }

        public string? GetIngredientUnit(string ingredient)
        {
            var unit = _context.Ingredients
                            .Where(i => string.Equals(i.Name, ingredient))
                            .Select(i => i.Unit)
                            .SingleOrDefault();
            return unit;   
        }

    }
}
