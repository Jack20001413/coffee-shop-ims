using CoffeeShopIMS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopIMS.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            this._ingredientRepository = ingredientRepository;
        }

        public ActionResult Index()
        {
            var ingredients = _ingredientRepository.GetAll();
            return View(ingredients);
        }

    }
}
