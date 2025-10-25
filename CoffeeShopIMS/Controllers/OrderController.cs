using System.Collections.ObjectModel;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using CoffeeShopIMS.Utils;
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
        private readonly IWarehouseRepository _warehouseRepository;

        public OrderController(IOrderRepository orderRepository, IIngredientRepository ingredientRepository, ISupplierRepository supplierRepository, IWarehouseRepository warehouseRepository)
        {
            _orderRepository = orderRepository;
            _ingredientRepository = ingredientRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
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
                LoadViewModel = new PurchaseRequestLoadViewModel
                {
                    Ingredients = new SelectList(_ingredientRepository.GetAll(), nameof(Ingredient.Id), nameof(Ingredient.Name)),
                    Vendors = new SelectList(_supplierRepository.GetAll(), nameof(Supplier.Name), nameof(Supplier.Name)),
                    Warehouses = new SelectList(_warehouseRepository.GetAll(), nameof(Warehouse.Id), nameof(Warehouse.Address))
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PurchaseRequestViewModel data)
        {
            // TODO: Being implemented
            var receivedData = data.ReceiveViewModel;
            var supplier = _supplierRepository.FindByName(receivedData.VendorName);
            var order = new PurchaseOrder
            {
                CreationDate = receivedData.CreationDate,
                OrderPerson = receivedData.OrderPerson,
                Supplier = supplier,
                OrderNumber = Randomizer.GenerateOrderCode(),
                UpdatedAt = DateTime.UtcNow,
                OrderDetails = receivedData.OrderedIngredients,
                WarehouseId = receivedData.WarehouseId
            };

            _orderRepository.Create(order);
            _orderRepository.CommitChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
