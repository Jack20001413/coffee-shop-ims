using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using CoffeeShopIMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeShopIMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ISupplierRepository _supplierRepository;

        public OrderController(IOrderRepository orderRepository, IIngredientRepository ingredientRepository, ISupplierRepository supplierRepository)
        {
            _orderRepository = orderRepository;
            _ingredientRepository = ingredientRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Create()
        {
            var model = new PurchaseRequestViewModel
            {
                Ingredients = new SelectList(_ingredientRepository.GetAll(), nameof(Ingredient.Id), nameof(Ingredient.Name)),
                Vendors = new SelectList(_supplierRepository.GetAll(), nameof(Supplier.Name), nameof(Supplier.Name))
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PurchaseRequestViewModel data)
        {
            // TODO: Not implemented yet
            return RedirectToAction(nameof(Index));
        }

    }
}
